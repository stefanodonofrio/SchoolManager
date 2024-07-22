Run the following command to spu-up the Database:
```
docker run --name postgres-teachers -e POSTGRES_PASSWORD=CambridgeDotNet24 -p 5433:5432 -d postgres
```