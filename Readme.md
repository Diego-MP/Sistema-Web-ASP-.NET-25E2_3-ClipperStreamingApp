#Migration 
dotnet ef migrations add InitialCreate --project ../ClipperStreamingApp.Infrastructure --startup-project .

#Update
dotnet ef database update --project ../ClipperStreamingApp.Infrastructure --startup-project .
