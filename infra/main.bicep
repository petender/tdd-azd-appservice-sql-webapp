targetScope = 'resourceGroup'

// ============================================================================
// Parameters
// ============================================================================

@description('Azure region for all resources')
param location string = 'eastus2'

@description('Environment name used in resource naming (e.g., demo, dev)')
@minLength(1)
@maxLength(10)
param environment string

@description('Project name for tagging')
param projectName string = 'appservice-sql-webapp'

@description('Principal ID of the deploying user. Azure Developer CLI populates this automatically.')
param principalId string = ''

@description('SQL Entra ID administrator login name (display name of the deploying user)')
param sqlAdminLogin string = 'azd-admin'

// ============================================================================
// Variables
// ============================================================================

var uniqueSuffix = uniqueString(resourceGroup().id)

var tags = {
  Environment: environment
  ManagedBy: 'Bicep'
  Project: projectName
  SecurityControl: 'Ignore'
}

// Resource naming — CAF conventions
var logAnalyticsName = 'log-${projectName}-${environment}'
var appInsightsName = 'appi-${projectName}-${environment}'
var kvName = 'kv-${take('appsql', 6)}-${take(environment, 3)}-${take(uniqueSuffix, 6)}'
var sqlServerName = 'sql-${projectName}-${environment}-${take(uniqueSuffix, 4)}'
var sqlDatabaseName = 'sqldb-${projectName}-${environment}'
var appServicePlanName = 'asp-${projectName}-${environment}'
var appServiceName = 'app-${projectName}-${environment}-${take(uniqueSuffix, 4)}'

// Role definition IDs
var keyVaultAdminRoleId = '00482a5a-887f-4fb3-b363-3b7fe8e74483'
var keyVaultSecretsUserRoleId = '4633458b-17de-408a-b874-0445c86b69e6'

// SQL connection string using Entra ID Managed Identity auth
var sqlConnectionString = 'Server=tcp:${sqlServerName}${az.environment().suffixes.sqlServerHostname},1433;Initial Catalog=${sqlDatabaseName};Authentication=Active Directory Managed Identity;Encrypt=True;TrustServerCertificate=False;'

// ============================================================================
// Module Deployments — Phase 1: Monitoring
// ============================================================================

module monitoring 'modules/monitoring.bicep' = {
  name: 'monitoring-deploy'
  params: {
    logAnalyticsName: logAnalyticsName
    appInsightsName: appInsightsName
    location: location
    tags: tags
  }
}

// ============================================================================
// Module Deployments — Phase 2: Security (Key Vault)
// ============================================================================

module keyVault 'modules/key-vault.bicep' = {
  name: 'key-vault-deploy'
  params: {
    name: kvName
    location: location
    tags: tags
    sqlConnectionString: sqlConnectionString
    logAnalyticsWorkspaceResourceId: monitoring.outputs.logAnalyticsResourceId
  }
}

// ============================================================================
// Module Deployments — Phase 3: Data (SQL Server + Database)
// ============================================================================

module sqlServer 'modules/sql-server.bicep' = {
  name: 'sql-server-deploy'
  params: {
    serverName: sqlServerName
    databaseName: sqlDatabaseName
    location: location
    tags: tags
    sqlAdminLogin: sqlAdminLogin
    sqlAdminObjectId: principalId
  }
}

// ============================================================================
// Module Deployments — Phase 4: Compute (App Service)
// ============================================================================

module appService 'modules/app-service.bicep' = {
  name: 'app-service-deploy'
  params: {
    planName: appServicePlanName
    appName: appServiceName
    location: location
    tags: tags
    appInsightsConnectionString: monitoring.outputs.appInsightsConnectionString
    sqlConnectionSecretUri: keyVault.outputs.sqlConnectionStringSecretUri
    logAnalyticsWorkspaceResourceId: monitoring.outputs.logAnalyticsResourceId
  }
}

// ============================================================================
// Role Assignments — App Service MI → Key Vault Secrets User
// ============================================================================

resource kv 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: kvName
}

resource appServiceKvSecretsUser 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(kv.id, appServiceName, keyVaultSecretsUserRoleId)
  scope: kv
  properties: {
    roleDefinitionId: subscriptionResourceId(
      'Microsoft.Authorization/roleDefinitions',
      keyVaultSecretsUserRoleId
    )
    principalId: appService.outputs.principalId
    principalType: 'ServicePrincipal'
  }
}

// ============================================================================
// Deployer Data Plane Access — Key Vault Administrator
// ============================================================================

resource deployerKvAdmin 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(principalId)) {
  name: guid(kv.id, principalId, keyVaultAdminRoleId)
  scope: kv
  properties: {
    roleDefinitionId: subscriptionResourceId(
      'Microsoft.Authorization/roleDefinitions',
      keyVaultAdminRoleId
    )
    principalId: principalId
    principalType: 'User'
  }
}

// ============================================================================
// Outputs
// ============================================================================

@description('App Service default hostname')
output appServiceUrl string = 'https://${appService.outputs.defaultHostname}'

@description('App Service name')
output appServiceName string = appService.outputs.resourceName

@description('SQL Server FQDN')
output sqlServerFqdn string = sqlServer.outputs.sqlServerFqdn

@description('SQL Database name')
output sqlDatabaseName string = sqlServer.outputs.sqlDatabaseName

@description('Key Vault name')
output keyVaultName string = keyVault.outputs.resourceName

@description('Log Analytics workspace name')
output logAnalyticsName string = monitoring.outputs.logAnalyticsName

@description('Application Insights name')
output appInsightsName string = monitoring.outputs.appInsightsName

@description('Resource group name')
output resourceGroupName string = resourceGroup().name

@description('App Service principal ID (for post-deploy SQL user creation)')
output appServicePrincipalId string = appService.outputs.principalId

@description('Webapp endpoint for azd')
output AZURE_WEBAPP_URL string = 'https://${appService.outputs.defaultHostname}'
