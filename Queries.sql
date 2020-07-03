--CREATION DES TABLEAUX

--Crée le tableau d'utilisateurs
CREATE TABLE Utilisateur (
    id INT AUTO_INCREMENT,
    nom VARCHAR(20) NOT NULL,
    surnom VARCHAR(20) NOT NULL,
    email VARCHAR(40),
    mdp VARCHAR(16),
    dette DECIMAL(5,2) DEFAULT(000.00), ---La dette ne peut jamais excéder 999.99

    PRIMARY KEY (id)
);

--Crée le tableau de produits
CREATE TABLE Produit (
    id INT AUTO_INCREMENT,
    nom VARCHAR(20) NOT NULL,
    cout DECIMAL(4,2) DEFAULT(00.00),
    stock INT DEFAULT(0),

    PRIMARY KEY (id)
);

--Crée le tableau de factures
--Doit être créé apres Utilisateur et pr
CREATE TABLE facture(
    id INT AUTO_INCREMENT,
    utilisateur INT,
    produit INT,
    horodate TIMESTAMP,

    PRIMARY KEY (id),
    FOREIGN KEY (utilisateur) REFERENCES Utilisateur(id) ON DELETE CASCADE,
    FOREIGN KEY (produit) REFERENCES Produit(id) ON DELETE CASCADE
);





--FONCTIONS D'INSERTION

--Ajoute un nouvel utilisateur à la DB
INSERT INTO Utilisateur(nom,surnom,email,mdp) VALUES(
    --L'ID d'utilisateur est donnée par le DB
    'nom',
    'surnom',
    'example@mail.com',
    'motDePasse'
    --La dette commence à 0.00$
);

--Ajoute un nouveau produit à la DB
INSERT INTO produits(nom,cout) VALUES(
    'Nom produit'
    0.00 --Valeur par défaut = 0.00, mais peut être changé
);

--Ajoute une facture à la DB
INSERT INTO facture(utilisateur, produit, horodate) VALUES(
    1, --id d'utilisateur
    2, --id du produit
    '2020-4-7' -- Timestamp, AAAA-MM-JJ HH:MM:SS
);




--AFFICHAGES

--Affiche les infos des utilisateurs du plus endetté au moins endetté
SELECT nom AS Utilisateur, surnom AS Surnom, email AS 'E-mail', dette AS Dette
	FROM membre
	ORDER BY dette DESC;

--Affiche les infos des produits en stock
SELECT nom AS 'Nom du produit',cout AS 'Cout',stock AS 'Quantite en stock'
FROM Produit
WHERE stock > 0
ORDER BY nom;


--Affiche les infos de toutes les factures
SELECT      facture.horodate AS 'Date d`achat', Utilisateur.nom AS Utilisateur, Produit.nom AS Produit
FROM        facture
JOIN        Utilisateur
ON          Utilisateur.id = facture.utilisateur
JOIN        Produit
ON          Produit.id = facture.produit
ORDER BY    horodate DESC;