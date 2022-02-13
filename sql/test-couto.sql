CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Categories" (
    "Id" uuid NOT NULL,
    "Name" varchar(200) NOT NULL,
    "Description" varchar(200) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
);

CREATE TABLE "Courses" (
    "Id" uuid NOT NULL,
    "Name" varchar(200) NOT NULL,
    "Description" varchar(200) NOT NULL,
    "ImgUrl" varchar(200) NOT NULL,
    "CategoryId" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Courses" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Courses_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id")
);

CREATE TABLE "Module" (
    "Id" uuid NOT NULL,
    "Name" varchar(200) NOT NULL,
    "Order" integer NOT NULL,
    "CourseId" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Module" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Module_Courses_CourseId" FOREIGN KEY ("CourseId") REFERENCES "Courses" ("Id")
);

CREATE TABLE "Lesson" (
    "Id" uuid NOT NULL,
    "Title" varchar(200) NOT NULL,
    "Description" varchar(200) NOT NULL,
    "ImgUrl" varchar(200) NOT NULL,
    "ModuleId" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Lesson" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Lesson_Module_ModuleId" FOREIGN KEY ("ModuleId") REFERENCES "Module" ("Id")
);

CREATE INDEX "IX_Courses_CategoryId" ON "Courses" ("CategoryId");

CREATE INDEX "IX_Lesson_ModuleId" ON "Lesson" ("ModuleId");

CREATE INDEX "IX_Module_CourseId" ON "Module" ("CourseId");

CREATE UNIQUE INDEX "IX_Module_Order" ON "Module" ("Order");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220206025610_init', '6.0.1');

COMMIT;

