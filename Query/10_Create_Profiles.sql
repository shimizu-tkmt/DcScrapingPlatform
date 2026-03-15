CREATE TABLE "Profiles" (
    "UserId" text NOT NULL,
    "SpreadsheetId" text,
    "UpdatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Profiles" PRIMARY KEY ("UserId"),
    CONSTRAINT "FK_Profiles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
