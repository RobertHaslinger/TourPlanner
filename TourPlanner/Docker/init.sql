DO $$
BEGIN
    CREATE USER tourplanner WITH PASSWORD 'admin';
    EXCEPTION WHEN DUPLICATE_OBJECT THEN
    RAISE NOTICE 'not creating user tourplanner -- it already exists';
END;
$$;

CREATE DATABASE TourPlanner;
GRANT ALL PRIVILEGES ON DATABASE TourPlanner TO tourplanner;
ALTER USER tourplanner WITH SUPERUSER;

DO $$
BEGIN
    CREATE SCHEMA "dev";
    EXCEPTION WHEN DUPLICATE_OBJECT THEN
    RAISE NOTICE 'not creating schema dev -- it already exists';
END;
$$;

CREATE SEQUENCE IF NOT EXISTS "Tours_Id_seq" INCREMENT 1 MINVALUE 1;
CREATE TABLE IF NOT EXISTS "dev"."Tours" (
    "Id" integer DEFAULT nextval('"Tours_Id_seq"') NOT NULL,
    "Name" character varying NOT NULL,
    "Description" character varying,
    "ImagePath" character varying,
    "StartLocation" character varying NOT NULL,
    "EndLocation" character varying NOT NULL,
    "HasTollRoad" boolean,
    "HasFerry" boolean,
    "HasSeasonalClosure" boolean,
    "HasHighway" boolean,
    "HasUnpaved" boolean,
    "HasCountryCross" boolean,
    "RouteTime" character varying,
    "Distance" double precision,
    "Fuel" double precision,
    CONSTRAINT "Tours_pkey" PRIMARY KEY ("Id")
) WITH (oids = false);

CREATE SEQUENCE IF NOT EXISTS "Logs_Id_seq" INCREMENT 1 MINVALUE 1;

CREATE TABLE IF NOT EXISTS "dev"."Logs" (
    "Id" integer DEFAULT nextval('"Logs_Id_seq"') NOT NULL,
    "Name" character varying NOT NULL,
    "Report" character varying NOT NULL,
    "Distance" double precision NOT NULL,
    "TotalTime" character varying NOT NULL,
    "Rating" character varying NOT NULL,
    "AverageSpeed" double precision NOT NULL,
    "Vehicle" character varying NOT NULL,
    "EnergyUnitUsed" double precision NOT NULL,
    "TourId" integer NOT NULL,
    CONSTRAINT "Logs_pkey" PRIMARY KEY ("Id")
) WITH (oids = false);

ALTER TABLE ONLY "dev"."Logs" ADD CONSTRAINT "constraint_fk" FOREIGN KEY ("TourId") REFERENCES "dev"."Tours"("Id") ON DELETE CASCADE NOT DEFERRABLE;