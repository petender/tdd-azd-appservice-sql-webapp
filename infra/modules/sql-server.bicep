// ============================================================================
// SQL Server Module — Logical SQL Server + SQL Database
// Entra ID admin with Azure AD-only authentication for the demo.
// ============================================================================

@description('SQL Server name')
param serverName string

@description('SQL Database name')
param databaseName string

@description('Azure region')
param location string

@description('Resource tags')
param tags object

@description('Entra ID admin login display name')
param sqlAdminLogin string

@description('Entra ID admin object ID (principal ID of deploying user)')
param sqlAdminObjectId string

// ============================================================================
// SQL Server (AVM)
// ============================================================================

module sqlServer 'br/public:avm/res/sql/server:0.10.0' = {
  name: '${serverName}-deploy'
  params: {
    name: serverName
    location: location
    tags: tags
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    administrators: {
      azureADOnlyAuthentication: true
      login: sqlAdminLogin
      principalType: 'User'
      sid: sqlAdminObjectId
      tenantId: tenant().tenantId
    }
    databases: [
      {
        name: databaseName
        sku: {
          name: 'Basic'
          tier: 'Basic'
          capacity: 5
        }
        maxSizeBytes: 2147483648
        collation: 'SQL_Latin1_General_CP1_CI_AS'
      }
    ]
    firewallRules: [
      {
        name: 'AllowAllAzureServices'
        startIpAddress: '0.0.0.0'
        endIpAddress: '0.0.0.0'
      }
    ]
  }
}

// ============================================================================
// Outputs
// ============================================================================

@description('SQL Server resource ID')
output resourceId string = sqlServer.outputs.resourceId

@description('SQL Server name')
output resourceName string = sqlServer.outputs.name

@description('SQL Server FQDN')
output sqlServerFqdn string = '${serverName}${environment().suffixes.sqlServerHostname}'

@description('SQL Database name')
output sqlDatabaseName string = databaseName
