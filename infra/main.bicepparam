using './main.bicep'

param environment = readEnvironmentVariable('AZURE_ENV_NAME', 'demo')
param location = readEnvironmentVariable('AZURE_LOCATION', 'eastus2')
param projectName = 'appservice-sql-webapp'
param principalId = readEnvironmentVariable('AZURE_PRINCIPAL_ID', '')
param sqlAdminLogin = 'azd-admin'
