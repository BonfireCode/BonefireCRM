# BonefireCRM

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



[SimpleCRM_Software_Requirements_Specification.docx](https://github.com/user-attachments/files/22031148/SimpleCRM_Software_Requirements_Specification.docx)

[SimpleCRM_Business_Requirements_Document.docx](https://github.com/user-attachments/files/22031150/SimpleCRM_Business_Requirements_Document.docx)

[SimpleCRM_Functional_Requirements_Document.docx](https://github.com/user-attachments/files/22031151/SimpleCRM_Functional_Requirements_Document.docx)

[SimpleCRM_Functional_Requirements_Document Deepseek generated.docx](https://github.com/user-attachments/files/22031157/SimpleCRM_Functional_Requirements_Document.Deepseek.generated.docx)

[SimpleCRM_Functional_Requirements_Document Gemini generated.docx](https://github.com/user-attachments/files/22031160/SimpleCRM_Functional_Requirements_Document.Gemini.generated.docx)

[CRM Mermaid DeepSeek Diagram.txt](https://github.com/user-attachments/files/22031165/CRM.Mermaid.DeepSeek.Diagram.txt)

[CRM MVP.md](https://github.com/user-attachments/files/22031295/CRM.MVP.md)

[Entity Relationship Diagram CRM.txt](https://github.com/user-attachments/files/22307332/Entity.Relationship.Diagram.CRM.txt)

