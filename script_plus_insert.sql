/*==============================================================*/
/* Nom de SGBD :  PostgreSQL 8                                  */
/* Date de création :  06/12/2022 16:19:40                      */
/*==============================================================*/

drop table if exists CATEGORIE_MATERIEL;

drop table if exists EST_ATTRIBUE;

drop table if exists MATERIEL;

drop table if exists PERSONNEL;

/*==============================================================*/
/* Table : CATEGORIE_MATERIEL                                   */
/*==============================================================*/
create table CATEGORIE_MATERIEL (
   IDCATEGORIE          SERIAL               not null,
   NOMCATEGORIE         VARCHAR(50)          null,
   constraint PK_CATEGORIE_MATERIEL primary key (IDCATEGORIE),
   constraint AK_IDENTIFIANT_2_CATEGORI unique (NOMCATEGORIE)
);

/*==============================================================*/
/* Index : CATEGORIE_MATERIEL_PK                                */
/*==============================================================*/
create unique index CATEGORIE_MATERIEL_PK on CATEGORIE_MATERIEL (
IDCATEGORIE
);

/*==============================================================*/
/* Table : EST_ATTRIBUE                                         */
/*==============================================================*/
create table EST_ATTRIBUE (
   IDPERSONNEL          INT4                 not null,
   IDMATERIEL           INT4                 not null,
   DATEATTRIBUTION      DATE                 not null,
   COMMENTAIREATTRIBUTION VARCHAR(1000)        null,
   constraint PK_EST_ATTRIBUE primary key (IDPERSONNEL, IDMATERIEL, DATEATTRIBUTION)
);

/*==============================================================*/
/* Index : EST_ATTRIBUE_PK                                      */
/*==============================================================*/
create unique index EST_ATTRIBUE_PK on EST_ATTRIBUE (
IDPERSONNEL,
IDMATERIEL,
DATEATTRIBUTION
);

/*==============================================================*/
/* Index : EST_ATTRIBUE_FK                                      */
/*==============================================================*/
create  index EST_ATTRIBUE_FK on EST_ATTRIBUE (
IDPERSONNEL
);

/*==============================================================*/
/* Index : EST_ATTRIBUE2_FK                                     */
/*==============================================================*/
create  index EST_ATTRIBUE2_FK on EST_ATTRIBUE (
IDMATERIEL
);

/*==============================================================*/
/* Table : MATERIEL                                             */
/*==============================================================*/
create table MATERIEL (
   IDMATERIEL           SERIAL               not null,
   IDCATEGORIE          INT4                 not null,
   NOMMATERIEL          VARCHAR(100)         null,
   REFERENCECONSTRUCTEURMATERIEL VARCHAR(100)         null,
   CODEBARREINVENTAIRE  VARCHAR(100)         null,
   constraint PK_MATERIEL primary key (IDMATERIEL),
   constraint AK_IDENTIFIANT_2_MATERIEL unique (CODEBARREINVENTAIRE)
);

/*==============================================================*/
/* Index : MATERIEL_PK                                          */
/*==============================================================*/
create unique index MATERIEL_PK on MATERIEL (
IDMATERIEL
);

/*==============================================================*/
/* Index : APPARTIENT_A_FK                                      */
/*==============================================================*/
create  index APPARTIENT_A_FK on MATERIEL (
IDCATEGORIE
);

/*==============================================================*/
/* Table : PERSONNEL                                            */
/*==============================================================*/
create table PERSONNEL (
   IDPERSONNEL          SERIAL               not null,
   EMAILPERSONNEL       VARCHAR(30)          null,
   NOMPERSONNEL         VARCHAR(20)          null,
   PRENOMPERSONNEL      VARCHAR(20)          null,
   constraint PK_PERSONNEL primary key (IDPERSONNEL),
   constraint AK_IDENTIFIANT_2_PERSONNE unique (EMAILPERSONNEL)
);

/*==============================================================*/
/* Index : PERSONNEL_PK                                         */
/*==============================================================*/
create unique index PERSONNEL_PK on PERSONNEL (
IDPERSONNEL
);

alter table EST_ATTRIBUE
   add constraint FK_EST_ATTR_EST_ATTRI_PERSONNE foreign key (IDPERSONNEL)
      references PERSONNEL (IDPERSONNEL)
      on delete cascade on update restrict;

alter table EST_ATTRIBUE
   add constraint FK_EST_ATTR_EST_ATTRI_MATERIEL foreign key (IDMATERIEL)
      references MATERIEL (IDMATERIEL)
      on delete cascade on update restrict;

alter table MATERIEL
   add constraint FK_MATERIEL_APPARTIEN_CATEGORI foreign key (IDCATEGORIE)
      references CATEGORIE_MATERIEL (IDCATEGORIE)
      on delete cascade on update restrict;


insert into CATEGORIE_MATERIEL (NOMCATEGORIE) Values
	('PC Fixe'),
	('PC Portable'),
	('Tablette');
	
insert into PERSONNEL (EMAILPERSONNEL, NOMPERSONNEL, PRENOMPERSONNEL) values
	('grugru@univ-smb.fr', 'GRUSON', 'Nathalie'),
	('bdiard@univ-smb.fr', 'DIARD', 'Benoit'),
	('meger@univ-smb.fr', 'MEGER', 'Nicolas'),
	('yoann@univ-smb.fr', 'GAILLARD', 'Yoann'),
	('vcout@univ_smb.fr', 'COUTURIER', 'Vincent');

insert into MATERIEL (IDCATEGORIE, NOMMATERIEL, REFERENCECONSTRUCTEURMATERIEL, CODEBARREINVENTAIRE) values
	(1, 'Poste 1 Salle D361', '1351624354', '31343sdth135t2dghgh'),
	(1, 'Poste 2 Salle D361', '1351624354', 'q5654g3dgs3t6'),
	(1, 'Poste 3 alle D361', '1351624354', '57rrgh25sd4h25stg'),
	(1, 'Poste 4 Salle D361', '1351624354', 's6ht4s64hst6'),
	(1, 'Poste 5 Salle D361', '1351624354', '1q3zrg4qrg13t'),
	(1, 'Poste 6 Salle D361', '1351624354', '3ZG5Q7S3TG'),
	(2, 'Armoire 2 position 1 Salle D362', '1351624354', 's35df4gs3f54g6'),
	(2, 'Armoire 2 position 2 Salle D362', '1351624354', 'sg23456hsdh24'),
	(2, 'Armoire 2 position 3 Salle D362', '1351624354', '3q54g3rse8g4e3'),
	(2, 'Armoire 2 position 4 Salle D362', '1351624354', 's3t5h54s3g4sdb4s'),
	(3, 'Armoire 1 position 1 Salle D362', '1351624354', 'q35g4r3b0ds54d'),
	(3, 'Armoire 1 position 2 Salle D362', '1351624354', '3qdf54h3xd54xh3sr'),
	(3, 'Armoire 1 position 3 Salle D362', '1351624354', 'whwtsxr5d5df5j4y');
	
insert into EST_ATTRIBUE (IDMATERIEL, IDPERSONNEL, DATEATTRIBUTION, COMMENTAIREATTRIBUTION) values
	(2, 3, '2023-06-14', 'PC connecter au VP de la salle'),
	(12, 2, '2023-06-15', 'Pour tester une application mobile'),
	(8, 4, '2023-06-17', 'Cable ethernet tombé en panne');

insert into EST_ATTRIBUE (IDMATERIEL, IDPERSONNEL, DATEATTRIBUTION) values
	(1, 1, '2023-06-18'),
	(5, 2, '2023-06-18'),
	(8, 1, '2023-06-19'),
	(8, 5, '2023-06-18');
	

select nompersonnel, prenompersonnel, emailpersonnel, 'est attribue a' as "est attribue a", nommateriel, nomcategorie, 'le' as "le", dateattribution
from est_attribue a
	join personnel p on a.IDPERSONNEL = P.IDPERSONNEL
	join materiel m on a.IDMATERIEL = m.IDMATERIEL
	join categorie_materiel c on m.IDCATEGORIE = c.IDCATEGORIE
	order by dateattribution
