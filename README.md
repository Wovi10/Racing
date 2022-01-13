# Begeleidend document
## Meegeleverde bestanden
Alle meegeleverde bestanden staan in het mapje “bestanden” in de root van het .zip bestand.
Door EF Core aangemaakt SQL-script genaamd ‘InitialCreate’.
Een zelfgemaakt SQL-scripts om de nodige data in de database te steken genaamd ‘BeginningValues’.
## Configuratie
1.	Run de twee meegeleverde SQL-bestanden in uw SQL Server Manager.
2.	Open de solution in uw IDE (vb. VS of Rider)
3.	Ga in de IDE naar uw terminal en ga naar de API folder genaamd “Racing” (dit doet u door ‘cd Racing’ te typen)
4.	Type ‘dotnet ef migrations add InitialCreate’
5.	Type ‘dotnet ef database update’
6.	Ga naar appsettings.json in de Racing folder
7.	Pas de servername aan naar de naam die voor u klopt (voorheen ‘LAPTOP-WOUT-V)  

Indien er nog geen Run/Debug configuraties zouden zijn maakt u deze aan als volgt:
 
1.	Klik op ‘Edit Configurations…’ 
2.	Zorg voor volgende instellingen
 
 
## Opstarten
Om het project op te starten kan u een IIS service opstarten runt u eerst de IIS Service voor de API en dan voor de UI.  
De pagina wordt opgestart in uw browser.
## Handleiding
Iedere entiteit kan op dezelfde manier gebruikt worden, het voorbeeld we gebruiken is “Pilot”.
### Bekijk alle items
Om alle items van de entiteit te bekijken klikt u op de naam.
### Voeg een item toe
Om een item toe te voegen, klikt u op “add new” en vult u alles in. Klik vervolgens op Create.
### Bewerk een item
Om een item te bewerken klikt u op “edit” en past u alles aan wat u wilt aanpassen. Klik vervolgens op update.
### Verwijder een item
Een verwijderen kan door op de knop “delete” te duwen.
### Terug naar home
U kan ten allen tijde terug naar de homepagina door links boven op “Racing” te klikken.
## Extra info
Ik heb in dit project geen inline query en stored procedure gebruikt omdat ik deze was vergeten en nu geen tijd meer heb.
Langst de andere kant is inline querying ook niet altijd even veilig dus is dit niet perse slecht.
