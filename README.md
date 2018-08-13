# Coding Dojo feladat
Ennek a feladatnak a célja, hogy a Témalabor keretében egy kicsit gyakoroljuk a TDD (Test Driven Development) eszközeit, egy kicsit cselesen megtekert formában.

## A feladat

A feladat kezdetben nagyon egyszerű, aztán ahogy haladunk előre és már nincs mit szépíteni a megoldáson, jönnek a vicces csavarok (mert a képzeletbeli megrendelőnek eszébe jutott valami). Így a mai agilis világ gondolkodásmódjához igazodva a szoftver design folyamatosan fejlődik, ahogy a követelmények is változnak.

A szoftverünk lényege egy Supermarket osztály, melynek `int Eval(string bag)` függvénye megkapja a vásárlói kosár teljes tartalmát, melyben nagybetűk jelzik az egyes termékeket. Pl. `"AABCCCD"` összesen 7 termék, 4 fajtából. A visszatérési érték pedig az ár, pl. 42.

Ehhez nyilván először olyan tesztek kellenek, hogy üres kosár ára 0, aztán valahogy kell tudni regisztrálni termékeket, aztán majd kiderül...

## A megoldás módja

Mivel itt most a tényleges megoldás a Supermarket osztály, így nem egy konzol alkalmazát készítünk, hanem egyből egy unit teszt alkalmazást, ami mellesleg magában foglalja a megoldást is. Konzolból lefuttatni ugyan nem lehet, de most a teszteken van a lényeg... majd ha készen leszünk, be lehet építeni egy nagy rendszerbe, ami kezeli a vonalkód leolvasókat, áruházi adatbázist meg minden egyebet.

A TDD lényege, hogy "test first", vagyis először mindig egy olyan tesztet írunk. A munka 3 fázisból áll:

- Piros fázis: Írunk egy tesztet, ami már éppen egy picivel többet vár el a rendszertől, mint amennyire az képes. De csak éppen annyival többet, hogy már piros legyen a teszt.
- Zöld fázis: Éppen csak annyit írunk a production kódhoz (a Supermarket osztályhoz), hogy minden teszt zöld legyen.
- Kék fázis: Most, hogy minden az aktuális specifikáció szerint megy, lehet szépítgetni, refactorálni a kódot. Ezt ilyenkor biztonságosan lehet végezni, mivel a tesztek folyamatosan ellenőrzik, hogy még minden rendben van-e.

Ha már nincs mit szépíteni a megoldáson, újra ugrunk a piros fázisra.

És most jön a játékos rész: ezt az egészet eloszva, felváltva csináljuk távolról, közben Slacken kommunikálva. Minden nap az éppen soron következő

- Megnézi a Slacket, hogy van-e valami újság.
- A git repositoryból pullolja az aktuális állást.
- Egy lépéssel előrébb viszi a megoldást.
- Pushol és megírja Slackre 1-2 mondatban, hogy mit csinált... meg ha valami eszébe jutott, észrevételek, tapasztalatok. (Egymás kódjából nagyon-nagyon sokat lehet tanulni!)

Az "egy lépéssel előrébb viszi a megoldást" a következő lehet:

- (Zöld fázis - egyszerű) Ha piros egy unit teszt, akkor azt bezöldíti és kész.
- ("Sárga-piros" fázis) Ha piros egy unit teszt, de úgy tűnik, ezt nem lehet egészen kevés (kb. 5 sor) kóddal bezöldíteni, akkor dönthet úgy is, hogy ez a teszt előre szaladt és (1) `[Ignored]`-ot ír a unit teszt elé (ettől az sárga lesz), és ír egy másikat, ami ugyanabba az irányba viszi a programot, de egy kisebb lépéssel. (Könnyen lehet, hogy az előző játékosnak nem jutott eszébe, hogyan is lehetne kisebbet lépni. Bizony van, hogy ez nagyon nem triviális.) Ilyenkor a következő játékos megint egy piros unit tesztet kap. Előbb utóbb azért törekszünk rá, hogy a felfüggesztett unit teszt is visszakerüljön és ő is bezöldüljön.
- (Kék fázis) Ha minden unit teszt zöld, de valami nem szép a kódban, akkor itt az ideje refactorálni, szépíteni. Erre is nagyon sok lehetőség van: kód duplikációk rendbe rakása, változók beszédesebbé átnevezése. Azért ha csak egy változót nevezel át, akkor menj egy kicsit tovább... vagy több refactorral, vagy egy piros fázissal.
- (Piros fázis) Ha minden teszt zöld és tökéletesnek érzed a kódot, akkor írj egy új unit tesztet, ami egy picivel többet vár el, mint amit tud a rendszer.

Nagyon fontos, hogy itt most "shared code ownership" van, vagyis igen, belenyúlunk egymás forráskódjába. Ez manapság az iparban is egyre inkább előjön és bizony van, ahol lelki súrlódásokat és megsértett egókat okoz. Igen, lesz olyan, hogy valaki átírja a tökéletesnek érzett kódodat. Nagyon fontos, hogy ezt konstruktív lépésnek értékeld. Ha pedig nem értessz egyet, akkor beszéljük meg! Könnyen lehet, hogy egy nagyon érdekes "vita" lesz belőle, amiből sokat tanulunk. Egyébként meg csak az nem hibázik, aki nem csinál semmit.

## Workflow

A munkát a Slacken koordináljuk. Készítünk majd egy előzetes beosztást, hogy ki melyik napon kerül sorra, de ezt kezeljétek flexibilisen: amit látjátok, hogy nem fog menni, Slacken szóljatok és megoldjuk. (A csapatmunkában az soha nem jó megoldás, hogy csak csendben elhallgatjuk aztán aznap nem történik semmi. Ha egy projekt csúszik, azt is az a legjobb, ha amint kiderül, azonnal jelezzük az érintetteknek.)

Az áttekinthetőség érdekében a témalabor Slack csoportjában létrehoztam 4 új emojit: :red:, :yellowred:, :green: és :blue:. Ezt oda lehet rakni a hozzászólás elé, amiben leírjátok, hogy épp mit "léptetek".

## Értékelés, tapasztalatok

Nagyon kíváncsi vagyok, hogy sül el a "játék". Remélem, tanulságos lesz. A végén megbeszéljük a tanultakat.
