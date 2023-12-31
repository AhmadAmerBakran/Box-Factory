﻿using Dapper;
using Npgsql;

namespace infrastructure
{
    public class CreateDataBase
    {
        private readonly NpgsqlDataSource _dataSource;
        private static readonly string RebuildScript = @"
CREATE SCHEMA IF NOT EXISTS box_factory;
CREATE TABLE IF NOT EXISTS box_factory.boxes
(
    id             INTEGER GENERATED BY DEFAULT AS IDENTITY,
    BoxName        TEXT,
    Price          DOUBLE PRECISION,
    BoxWidth       DOUBLE PRECISION,
    BoxLength      TEXT,
    BoxHeight      DOUBLE PRECISION,
    BoxThickness   DOUBLE PRECISION,
    BoxColor       TEXT,
    BoxImgUrl       TEXT,
    PRIMARY KEY (id)
);";

        public CreateDataBase(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public void SetupDatabase()
        {
            using (var conn = _dataSource.OpenConnection())
            {
                try
                {
                    conn.Execute(RebuildScript);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to set up the database.", e);
                }
            }
        }
    }
}