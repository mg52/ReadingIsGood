# ReadingIsGood

This is a .net 5 sample project

You can find api documentation on swagger: https://mg52rig.herokuapp.com/swagger/index.html

You need to create customer first. if you create customer with user role "Admin", he/she can create product, stock records and can execute orders.
If you are a regular user, role need to be "User".

Api uses PostgreSQL (heroku free tier) and api is deployed to heroku using docker container using below commands:
Firstly create <heroku-app-name> App on heroku dashboard.
1. docker build -t <docker-image-name> .
2. docker tag <docker-image-name> registry.heroku.com/<heroku-app-name>/web
3. heroku login
4. heroku container:login
5. heroku container:push web -a <heroku-app-name>
6. heroku container:release web -a <heroku-app-name>
