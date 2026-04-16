-- ============================
-- ACTORES
-- ============================

MERGE INTO Actors AS target
USING (VALUES
('11111111-1111-1111-1111-111111111111','Keanu','Reeves','1964-09-02'),
('22222222-2222-2222-2222-222222222222','Carrie-Anne','Moss','1967-08-21'),
('33333333-3333-3333-3333-333333333333','Laurence','Fishburne','1961-07-30'),
('44444444-4444-4444-4444-444444444444','Hugo','Weaving','1960-04-04'),
('55555555-5555-5555-5555-555555555555','Ian','McShane','1942-09-29'),
('66666666-6666-6666-6666-666666666666','Halle','Berry','1966-08-14'),
('77777777-7777-7777-7777-777777777777','Tom','Hanks','1956-07-09'),
('88888888-8888-8888-8888-888888888888','Leonardo','DiCaprio','1974-11-11'),
('99999999-9999-9999-9999-999999999999','Scarlett','Johansson','1984-11-22'),
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa','Brad','Pitt','1963-12-18')
) AS source (Id, FirstName, LastName, Birthday)
ON target.Id = source.Id
WHEN NOT MATCHED THEN
INSERT (Id, FirstName, LastName, Birthday)
VALUES (source.Id, source.FirstName, source.LastName, source.Birthday);

-- ============================
-- PELÍCULAS (GUID válidos)
-- ============================

MERGE INTO Films AS target
USING (VALUES
('aaaaaaaa-0000-0000-0000-000000000001','The Matrix','1999-03-31'),
('aaaaaaaa-0000-0000-0000-000000000002','John Wick','2014-10-24'),
('aaaaaaaa-0000-0000-0000-000000000003','John Wick 2','2017-02-10'),
('aaaaaaaa-0000-0000-0000-000000000004','Inception','2010-07-16'),
('aaaaaaaa-0000-0000-0000-000000000005','Fight Club','1999-10-15'),
('aaaaaaaa-0000-0000-0000-000000000006','Forrest Gump','1994-07-06'),
('aaaaaaaa-0000-0000-0000-000000000007','Lucy','2014-07-25'),
('aaaaaaaa-0000-0000-0000-000000000008','The Matrix Reloaded','2003-05-15'),
('aaaaaaaa-0000-0000-0000-000000000009','The Matrix Revolutions','2003-11-05'),
('aaaaaaaa-0000-0000-0000-000000000010','Cast Away','2000-12-22')
) AS source (Id, Name, PremierDate)
ON target.Id = source.Id
WHEN NOT MATCHED THEN
INSERT (Id, Name, PremierDate)
VALUES (source.Id, source.Name, source.PremierDate);

-- ============================
-- RELACIONES (GUID válidos)
-- ============================

MERGE INTO FilmActor AS target
USING (VALUES
('aaaaaaaa-0000-0000-0000-000000000001','11111111-1111-1111-1111-111111111111'),
('aaaaaaaa-0000-0000-0000-000000000001','22222222-2222-2222-2222-222222222222'),
('aaaaaaaa-0000-0000-0000-000000000001','33333333-3333-3333-3333-333333333333'),

('aaaaaaaa-0000-0000-0000-000000000002','11111111-1111-1111-1111-111111111111'),
('aaaaaaaa-0000-0000-0000-000000000002','55555555-5555-5555-5555-555555555555'),

('aaaaaaaa-0000-0000-0000-000000000003','11111111-1111-1111-1111-111111111111'),
('aaaaaaaa-0000-0000-0000-000000000003','66666666-6666-6666-6666-666666666666'),

('aaaaaaaa-0000-0000-0000-000000000004','88888888-8888-8888-8888-888888888888'),

('aaaaaaaa-0000-0000-0000-000000000005','aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'),

('aaaaaaaa-0000-0000-0000-000000000006','77777777-7777-7777-7777-777777777777'),

('aaaaaaaa-0000-0000-0000-000000000007','99999999-9999-9999-9999-999999999999'),

('aaaaaaaa-0000-0000-0000-000000000008','11111111-1111-1111-1111-111111111111'),

('aaaaaaaa-0000-0000-0000-000000000009','11111111-1111-1111-1111-111111111111'),

('aaaaaaaa-0000-0000-0000-000000000010','77777777-7777-7777-7777-777777777777')
) AS source (FilmId, ActorId)
ON target.FilmModelsId = source.FilmId AND target.ActorModelsId = source.ActorId
WHEN NOT MATCHED THEN
INSERT (FilmModelsId, ActorModelsId)
VALUES (source.FilmId, source.ActorId);