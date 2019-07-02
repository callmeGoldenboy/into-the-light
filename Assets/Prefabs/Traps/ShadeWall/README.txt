README - SHADE WALL FOLDER


ANVÄNDNING:
	Dra ut prefaben till scenen. Ge Shade Wall objektet's WallMove.cs skript 
	en referens till spelaren. Se också till att ge kameran en referens till
	Shade Wall objektet.
	
	
INNEHÅLL:
>OvalMove.cs
	Detta skript är avsett exclusivt för händerna i dödsväggen, den hanterar 
    	händernas cirkulära rörelse och rotationer något slumpmässigt, så att man 
    	sällan ser två händer som beter sig likadant. In-datan som krävs är 
    	en relativ vektor från bildens centrum till punkten på bilden (Justera denna 
	vektor varje gång en ny bild används med skriptet), Ett gränsavstånd där 
	intensiteten skall öka och ett värde för hur mycket	intensiteten kan öka.
	Oval Move vänder också på en slumpmässig hälft av bilderna upp och ned.

>PlayerReference.cs
	Detta skript används för att ge en referens till spelaren åt all skript i prefaben
	som behöver det. För att implementera shadewall krävs alltså endast att man
	anger spelaren åt detta skript. (förutom att kameran också behöver en referens till
	shadewallen)
	
>Shade.png
	Genomskynlig bild som tänkte användas som vägg där händerna hamnar bakom,
	fungerade dock inte jättebra. Testa om ni vill genom att dra in bilden i
	sprite rendern i Shade Wall objektet och justera kameran lite.
	
>ShadeWall.prefab
	Själva dödsväggen. Har 5 direkta barn, 3 shade-grupper med olika vertikala
	rörelser (med Looped Move skriptet), och 2 extra lager av dödsväggar, 
	(utan killboxar) som båda har 3 egna shade-grupper som direkta barn.
	Varje shade-grupp har 5 "Shand"s (kort för "Shade Hand"), med olika bilder
	och var sitt Oval Move skript, så att alla (5*3*3 = 45) Shands rör sig olika.

>Shand <X>.prefab
	Prefabs som måste göras för varje bild. Bilderna är förvägsroterade så att
	börjar pekandes rakt fram (med vissa undantag). Siffran och tecknena i X
	noterar hur står rotation de har givits, förutom att separera namnen.
	Dessa prefabs ska innehålla OvalMove skriptet med en ordentlig rot-vektor.
	Ändra på dessa prefabs för att snabt modifiera hela dödsväggen.
	
>WallMove.cs	
	Algoritmen manipulerar obejktets position så att den rör sig mot en 
	referenspunkt. (Spelaren) Vanligtvis rör sig väggen med konstant hastighet,
	men skulle spelaren komma för långt bort börjar den kompensera.
	
>WhiteShade.png
	Vit variant av Shade.png.