# Fridge

## Version 0.1

Ce programme a pour but de de gérer les achats et les dettes pour le Frigo du Comic.

## Current Features

1. Utilisateur
    
    * Se connecter à son compte
    * Voir sa dette
    * Voir quels produits sont présentement en stock
    * Voir son historique d'achat

2. Admin
    * Créer, modifier les informations des produits dans le Fridge
        * Changer le nom, le prix et la quantité en stock d'un produit
    * Créer, modifier des comptes utilisateurs
        * Changer le nom, surnom, mot de passe et addresse email associés à un compte
        * Modiffier la dette associée à un compte

## TO-DO list

1. Release
    * Pour le moment, le code doit être exécuté en debug mode sur l'Éditeur de Godot Engine (non-fourni), il faudra donc l'exporter en HTML ou exe pour la release "officiele" - [ ]
1. MYSQL server
    * Il faudra établir un serveur MySQL 5.7 sur un ordinateur pour stocker les données des items factures et utilisateurs - [ ]
        * La version 8.0 n'est pas compatible avec ce programme 


1. Admin
    * Envoyer des emails au comptes avec des dettes - [ ]
        * Manuellement - [ ]
        * Automatiquement - [ ]
            * lorsqu'un utilisateur atteint une certaine valeur - [ ]
            * Après un certain intervalle - [ ]
1. Stats
    * Ajouter des statistiques supplémentaires

## Known bugs
