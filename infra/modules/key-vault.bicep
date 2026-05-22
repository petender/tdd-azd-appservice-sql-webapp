// ============================================================================
// Key Vault Module — Secure secret storage with RBAC authorization
// Stores the SQL connection string; App Service retrieves via Key Vault reference.
// ============================================================================

@description('Key Vault name (max 24 chars)')
param name string

@description('Azure region')
param location string

@description('Resource tags')
param tags object

@description('SQL connection string to store as a secret')
@secure()
param sqlConnectionString string

@description('Log Analytics workspace resource ID for diagnostic settings')
param logAnalyticsWorkspaceResourceId string

// ============================================================================
// Key Vault (AVM)
// ============================================================================

module keyVault 'br/public:avm/res/key-vault/vault:0.11.0' = {
  name: '${name}-deploy'
  params: {
    name: name
    location: location
    tags: tags
    enableRbacAuthorization: true
    enablePurgeProtection: false
    enableSoftDelete: true
    softDeleteRetentionInDays: 7
    sku: 'standard'
    secrets: [
      {
        name: 'sql-connection-string'
        value: sqlConnectionString
      }
    ]
    diagnosticSettings: [
      {
        workspaceResourceId: logAnalyticsWorkspaceResourceId
        logCategoriesAndGroups: [
          { categoryGroup: 'allLogs', enabled: true }
        ]
        metricCategories: [
          { category: 'AllMetrics', enabled: true }
        ]
      }
    ]
  }
}

// ============================================================================
// Outputs
// ============================================================================

@description('Key Vault resource ID')
output resourceId string = keyVault.outputs.resourceId

@description('Key Vault name')
output resourceName string = keyVault.outputs.name

@description('Key Vault URI')
output vaultUri string = keyVault.outputs.uri

@description('SQL connection string secret URI (versioned)')
output sqlConnectionStringSecretUri string = '${keyVault.outputs.uri}secrets/sql-connection-string'
