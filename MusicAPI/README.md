# MusicAPI

MusicAPI är en tjänst som ger användaren information och diskografi med 
tillhörande albumomslagslänkar om valfri artist eller musikgrupp som 
Json.

## Installation

Zippa upp filen med programfilerna på valfri plats, starta sedan filen 
MusicAPI.csproj som ni då fått i er mapp. Då bör Visual Studio starta 
och projektet laddas in. Starta sedan tjänsten lokalt på er dator genom 
att trycka på den gröna Play-knappen högst uppe på skärmen. Nu skall APIt 
vara igång lokalt på er dator och ni kan nu använda det.

## Användning

När APIt är i drift så söker ni på artister genom valfri API-hanterare 
eller i er webbläsare. Eftersom tjänsten än så länge bara är till för 
demonstration så kommer det att köras som en local host och då kommer 
adressen ni skall göra era anrop mot vara 
https://localhost:xxxxx/api/artist/(följt av valfritt MusicBrainz-ID) 
där xxxxx kommer att vara ett unikt nr för just er, numret går att se 
när ni startat tjänsten i Visual Studio i det extra webbfönster som bör 
ha öppnats.

Ex. på MBID är Roxette: d3b2711f-2baa-441a-be95-14945ca7e6ea
			   Rihanna: 73e5e69d-3554-40d8-8516-00cb38737a1c

## Felhantering

I de fall där de externa APIerna inte kan svara med den information som
MusicAPI önskar så skickas den externa APIns statuskod tillbaka till 
användaren med information om vilken typ av fel som inträffat och i
vilken tjänst. Undantaget är om CoverArtArchive inte har det skivomslag
MusicAPI frågar efter, i de fallen returneras "No cover found" istället 
för URL-adressen på det specifika albumet.

## Uppgifsbeskrivning

Denna uppgift går ut på att skapa ett REST API som erbjuder en mashup 
av några bakomliggande API:er. De API:er som ska kombineras är 
MusicBrainz, Wikidata/Wikipedia och Cover Art Archive. 

● MusicBrainz erbjuder ett API med bland annat detaljerad information 
om musikartister (information såsom artistens namn, födelseår, 
födelseland osv). 
● Wikipedia är en community-wiki som innehåller beskrivande information 
om bland annat just musikartister. Ibland länkar MusicBrainz till 
Wikipedia, men oftast så länkar de till Wikidata som fungerar som en 
språkproxy mot Wikipedia. Wikidata innehåller alltså information om 
alla olika Wikipedia-länkar (svenska, engelska, franska osv). 
● Cover Art Archive är ett systerprojekt till MusicBrainz som innehåller 
omslagsbilder till olika album, singlar eps osv som släppts av en artist.

Ditt API - själva uppgiften - ska ta emot ett MBID(MusicBrainz Identifier)
och leverera tillbaka ett resultat bestående av: 

● En beskrivning av artisten som hämtas från Wikipedia. Wikipedia innehåller
inte några MBID utan mappningen mellan MBID och Wikipedia-identifierare 
finns via MusicBrainz API 
(antingen en direktreferens eller via språkproxyn Wikidata). 
● En lista över alla album som artisten släppt och länkar till bilder för 
varje album. Listan över album finns i MusicBrainz men bilderna finns på 
Cover Art Archive.