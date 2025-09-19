# 📌 BonefireCRM

[![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet)](https://dotnet.microsoft.com/)  
A lightweight, modular CRM system built with **.NET 9** — focused on **Contacts, Companies, Deals, Activities, and Follow-ups**. 

---

## ✨ Features
- 👤 **Contacts & Companies** — Manage B2B and B2C relationships.  
- 💼 **Deals & Pipelines** — Track opportunities across pipeline stages.  
- 📅 **Activities** — Calls, meetings, notes all in one place.  
- ⏰ **Follow-ups** — Smart reminders & auto-generated tasks.  

---

## 🏗️ Tech Stack
- **Backend:** .NET 9, Entity Framework Core  
- **Database:** SQLLITE 
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
<img width="3840" height="2210" alt="ERD" src="https://github.com/user-attachments/assets/da696a67-ee7c-44e5-8601-2b4b1c072baf" />


## 🔄 Core Flows

### 1️⃣ Contacts & Companies
   [Contacts & Companies Flow.txt](https://github.com/user-attachments/files/22324050/Contacts.Companies.Flow.txt)
   <img width="2204" height="3840" alt="Contact   Companies Flow" src="https://github.com/user-attachments/assets/e1c8bef2-5936-4774-aee0-f9453ffbcedf" />


### 2️⃣ Deals & Pipelines
   [Deals & Pipelines Flow.txt](https://github.com/user-attachments/files/22324142/Deals.Pipelines.Flow.txt)
   <img width="2309" height="3840" alt="Deals   Pipelines Flow" src="https://github.com/user-attachments/assets/944868ef-ad56-45a3-851d-c6b335f85780" />


### 3️⃣ Activities
   [Activities Flow.txt](https://github.com/user-attachments/files/22324049/Activities.Flow.txt)
   <img width="3621" height="3840" alt="Activities Flow" src="https://github.com/user-attachments/assets/1f88f732-5341-4d5a-a069-10672b42fc27" />

### 4️⃣ Follow-ups
   [Follow-up Reminders Flow.txt](https://github.com/user-attachments/files/22324048/Follow-up.Reminders.Flow.txt)
   <img width="1606" height="3840" alt="Follow-up Reminders flow" src="https://github.com/user-attachments/assets/10cafba0-1871-4824-8dc3-a0d0545a91cd" />



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


