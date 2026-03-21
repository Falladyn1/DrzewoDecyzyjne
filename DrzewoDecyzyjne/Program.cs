using DrzewoDecyzyjne;

ZbiorDanych baza1 = new ZbiorDanych();
baza1.wczytajDane("iris.data");

//baza1.drukujDane();

Drzewo drzewo = new Drzewo();

drzewo.UtworzDrzewo(baza1, 30);

drzewo.WypiszDrzewo();