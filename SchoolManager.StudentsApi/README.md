Run the following command to spu-up the Database:
```
docker run --name postgres-students -e POSTGRES_PASSWORD=CambridgeDotNet24 -p 5435:5432 -d postgres
```