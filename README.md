
- **Back-end** : Développé avec .NET Core, garantissant des performances élevées, une sécurité renforcée, et une extensibilité facile pour intégrer de nouvelles fonctionnalités.
  
- **Swagger** : Pour tester les Api (l'ajout/update/delete/getAll/getById).
  
- **Base de données** : Postgres est utilisé pour stocker et organiser efficacement les données des Sensors ,des transactions et des interactions.
  
-**Versionning de l'api**
  
-**MemoryCache**
  
 -**UnitTest:xUint & MOCK**


## Architecture
 

<img src="captures Sensor/archi.png"  width="600">  

## Authentification  
Une authentification basique pour se connecter et accéder à l'application.  

<img src="captures Sensor/result-authen.PNG"  width="600">  

---
## Page principal de Swagger qui contient les api sont crées 
*authentification basique
*Add
*update
*getAll
*getById
*delete

<img src="captures Sensor/List api swager.PNG"  width="600">  
<img src="captures Sensor/post-api-param.PNG"  width="600">
<img src="captures Sensor/api-delete.PNG"  width="600">
<img src="captures Sensor/get all.PNG"  width="600">
---
## Tests Unitaires avec xUnit et Moq  
Les tests unitaires ont été implémentés en utilisant **xUnit** et **Moq** pour simuler les dépendances et valider le comportement des API. Ces tests assurent que les fonctionnalités fonctionnent comme prévu et réduisent les risques d'erreurs.  

<img src="captures Sensor/untiTest.PNG" alt="Unit Test Execution" width="600">  

---

## Versioning de l'API  
Un mécanisme de versioning a été mis en place pour gérer plusieurs versions de l'API. Cela garantit une compatibilité ascendante tout en permettant des améliorations sans perturber les clients existants.  

<img src="captures Sensor/api_versioning.PNG" alt="API Versioning Swagger" width="600">  

---

## Mise en Cache  
L'utilisation de **IMemoryCache** améliore les performances en évitant les requêtes redondantes vers la base de données.  

---

## Personnalisation des Messages d’Erreur  
Des messages d’erreur personnalisés ont été ajoutés pour améliorer l'expérience utilisateur en rendant les réponses plus explicites.  


---

## Mise à Jour de la Base de Données  
Les migrations permettent de synchroniser les modèles de données avec la base de données **Postgres**.  

<img src="captures Sensor/before-update.PNG" alt="Database Migration" width="600">  




