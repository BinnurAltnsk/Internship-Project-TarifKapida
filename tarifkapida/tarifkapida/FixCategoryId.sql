-- CategoryId alanını nullable yapmak için SQL komutu
USE TarifKapida;

-- Mevcut CategoryId alanını nullable yap
ALTER TABLE RECIPE 
ALTER COLUMN CategoryId INT NULL;

-- Eğer mevcut kayıtlarda CategoryId NULL ise, varsayılan bir değer ata
-- (Bu komutu sadece gerekirse çalıştırın)
-- UPDATE RECIPE SET CategoryId = 1 WHERE CategoryId IS NULL;

PRINT 'CategoryId alanı başarıyla nullable yapıldı.'; 