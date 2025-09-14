# 📌 BonefireCRM

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/)  
A lightweight, modular CRM system built with **.NET 8** — focused on **Contacts, Companies, Deals, Activities, and Follow-ups**. 

---

## ✨ Features
- 👤 **Contacts & Companies** — Manage B2B and B2C relationships.  
- 💼 **Deals & Pipelines** — Track opportunities across pipeline stages.  
- 📅 **Activities** — Calls, meetings, notes all in one place.  
- ⏰ **Follow-ups** — Smart reminders & auto-generated tasks.  

---

## 🏗️ Tech Stack
- **Backend:** .NET 8, Entity Framework Core  
- **Database:** SQLLITE (default, but swappable)  
- **Diagrams:** Mermaid.js for ERD & flows
- **Versioning:** GitHub  

---

## 🗂️ Documentation

[SimpleCRM_Software_Requirements_Specification.docx](https://github.com/user-attachments/files/22031148/SimpleCRM_Software_Requirements_Specification.docx)

[SimpleCRM_Business_Requirements_Document.docx](https://github.com/user-attachments/files/22031150/SimpleCRM_Business_Requirements_Document.docx)

[SimpleCRM_Functional_Requirements_Document.docx](https://github.com/user-attachments/files/22031151/SimpleCRM_Functional_Requirements_Document.docx)

[SimpleCRM_Functional_Requirements_Document Deepseek generated.docx](https://github.com/user-attachments/files/22031157/SimpleCRM_Functional_Requirements_Document.Deepseek.generated.docx)

[SimpleCRM_Functional_Requirements_Document Gemini generated.docx](https://github.com/user-attachments/files/22031160/SimpleCRM_Functional_Requirements_Document.Gemini.generated.docx)

[CRM Mermaid DeepSeek Diagram.txt](https://github.com/user-attachments/files/22031165/CRM.Mermaid.DeepSeek.Diagram.txt)

[CRM MVP.md](https://github.com/user-attachments/files/22031295/CRM.MVP.md)

📊 Entity Relationship Diagram (ERD)

[Entity Relationship Diagram CRM.txt](https://github.com/user-attachments/files/22307332/Entity.Relationship.Diagram.CRM.txt)

## 🔄 Core Flows

### 1️⃣ Contacts & Companies
   [Contacts & Companies Flow.txt](https://github.com/user-attachments/files/22324050/Contacts.Companies.Flow.txt)

### 2️⃣ Deals & Pipelines
   [Deals & Pipelines Flow.txt](https://github.com/user-attachments/files/22324142/Deals.Pipelines.Flow.txt)

### 3️⃣ Activities
   [Activities Flow.txt](https://github.com/user-attachments/files/22324049/Activities.Flow.txt)

### 4️⃣ Follow-ups
   [Follow-up Reminders Flow.txt](https://github.com/user-attachments/files/22324048/Follow-up.Reminders.Flow.txt)


## ⚙️ Developer Setup

Create migrations:
```powershell
dotnet ef migrations add InitialModel --context CRMContext -p ../BonefireCRM.Infrastructure/BonefireCRM.Infrastructure.csproj -s BonefireCRM.API.csproj -o Migrations
```

Apply migrations:

```powershell
dotnet ef database update -c CRMContext -p ../BonefireCRM.Infrastructure/BonefireCRM.Infrastructure.csproj -s BonefireCRM.API.csproj
```

Generate script:

```powershell
dotnet ef migrations script --context CRMContext -p ../BonefireCRM.Infrastructure/BonefireCRM.Infrastructure.csproj -s BonefireCRM.API.csproj | out-file ./script.sql
```


