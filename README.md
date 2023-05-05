# YetAnotherCryptography

Ist eine unsichere und simple XOR-Cipher.

* YetAnotherCryptography.DLL
  * Beinhaltet alles wichtige zum Verschlüsseln und Entschlüsseln
* YetAnotherCryptography.Desktop
  * Beinhaltet die CLI Anwendung, die über cmd.exe ausgeführt werden kann
* YetAnotherCryptography.Test
  * NUnit Tests, aber unvollständig


Erwarte keine Meisterleistung und führe im Root Verzeichnes der \*.exe Datei folgenden Befehl in der Konsole aus:
```
"Hallo Welt" > test.txt
```
Und dann folgenden Befehl:
```
YetAnotherCryptography.Desktop.exe s "test.txt" -se p 123
```
Es sollte dann eine Datei mit folgendem Namen erscheinen: `test.txt.yac` (yac = YetAnotherCryptography oder so)

Öffne die Datei im Editor. Du solltest ein Brei von Symbolen, Buchstaben und Zahlen erkennen.
Nun lösche die alte `test.txt` Datei mit folgendem Befehl:
```
del test.txt
```
Nun führe folgendem Befehl aus:
```
YetAnotherCryptography.Desktop.exe s "test.txt.yac" -sd p 123
```
Jetzt solltest du erneut eine `test.txt` sehen. Wenn alles richtig verlaufen ist, müsstest du folgenden Text in der Textdatei sehen können: `Hallo Welt`

Hier nochmal ein Überblick aller Argumente:
```
YetAnotherCryptography.Desktop.exe s "FILE" -se p "PASSWORT"
YetAnotherCryptography.Desktop.exe s "FILE" -sd p "PASSWORT"
YetAnotherCryptography.Desktop.exe s "DIR" -de p "PASSWORT"
YetAnotherCryptography.Desktop.exe s "DIR" -dd p "PASSWORT"
YetAnotherCryptography.Desktop.exe -h
```
```
-se = SOURCE ENCRYPT
-sd = SOURCE DECRYPT
-de = DIRECTORY ENCRYPT
-dd = DIRECTORY DECRYPT
 -h = HELP
  p = PASSWORT
  s = SOURCE
```
