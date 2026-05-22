<!-- markdownlint-disable MD033 MD041 -->

# 🏗️ App Service SQL WebApp

**A fully PaaS demo scenario deploying an Azure App Service web application with Azure SQL Database backend for a logistics shipment tracking application. Demonstrates N-Tier architecture with Managed Identity, Key Vault secrets management, and Application Insights monitoring — all orchestrated via Azure Developer CLI (azd).**

💪 This template scenario is part of the larger [Microsoft Trainer Demo Deploy Catalog](https://aka.ms/trainer-demo-deploy).

---

## ⬇️ Installation

[Azure Developer CLI - AZD](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd)
When installing AZD, the above the following tools will be installed on your machine as well, if not already installed:

- [GitHub CLI](https://cli.github.com/)
- [Bicep CLI](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/install)

You need Owner or Contributor access permissions to an Azure Subscription to deploy the scenario.

## 🚀 Deploying the scenario in 4 steps

1. Create a new folder on your machine.

```shell
mkdir appservice-sql-webapp
```

2. Next, navigate to the new folder.

```shell
cd appservice-sql-webapp
```

3. Next, run azd init to initialize the deployment.

```shell
azd init -t appservice-sql-webapp
```

4. Last, run azd up to trigger an actual deployment.

```shell
azd up
```

⏩ Note: you can delete the deployed scenario from the Azure Portal, or by running `azd down` from within the initiated folder.

## What is the demo scenario about?

This scenario deploys a PaaS web application for a logistics company. An Azure App Service hosts a .NET 10 web application that provides CRUD operations for managing shipments, warehouses, and drivers. Azure SQL Database (Basic tier) stores the logistics data. Key Vault secures connection strings, Application Insights provides telemetry, and all resources send diagnostics to a Log Analytics Workspace. The entire architecture is serverless PaaS — no VMs, no IaaS.

## 📋 Project Summary

| Property         | Value                   |
| ---------------- | ----------------------- |
| **Created**      | 2026-04-28              |
| **Last Updated** | 2026-04-28              |
| **Region**       | `eastus2`               |
| **Environment**  | Demo                    |

## 📊 Progress

| Step | Artifact                     | Status    |
| ---- | ---------------------------- | --------- |
| 1    | Requirements                 | ✅ Done   |
| 2    | Architecture Assessment      | ✅ Done   |
| 3    | Architecture Diagrams        | ✅ Done   |
| 4    | Implementation Plan + Bicep  | ⏳ Pending |
| 4b   | Sample Web Application       | ⏳ Pending |
| 5    | Deployment                   | ⏳ Pending |
| 6    | Demo Guide                   | ⏳ Pending |

---
