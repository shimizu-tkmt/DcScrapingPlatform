CREATE TABLE "DcCredentials" (
    "UserId" text NOT NULL,
    "EncryptedId" bytea NOT NULL,
    "EncryptedPassword" bytea NOT NULL,
    "Iv" bytea NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_DcCredentials" PRIMARY KEY ("UserId"),
    CONSTRAINT "FK_DcCredentials_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
