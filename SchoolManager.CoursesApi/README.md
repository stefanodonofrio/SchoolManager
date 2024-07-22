Run the following command to spu-up the Database:
```
docker run --name postgres-courses -e POSTGRES_PASSWORD=CambridgeDotNet24 -p 5434:5432 -d postgres
```