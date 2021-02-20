# VR-Klassenzimmer

## Bedienungsanleitung (master branch)

### Setup der Brille
Schließen Sie zuerst das Head mounted Display (HMD) an. 
Starten Sie SteamVR über Steam.
Sofern vorhanden, starten sie nun TPCast oder eine andere kabellose Erweiterung für ihr HMD.
Messen Sie eine 2m x 2m freie Fläche aus, die sogenannte Play area.

### Starten des Klassenzimmers
Installieren Sie unityHub.
Clonen Sie das Repository auf Ihren Rechner und fügen Sie es in unityHub zu Ihren Projekten hinzu. Hier wird Ihnen die derzeitige Version angezeigt, die für das Projekt notwendig ist.
Installieren Sie die angegebene Version von unity.
Starten Sie das Projekt in unity. Dieser Schritt wird einen Moment dauern, da unity ein paar Einstellungen vornehmen muss.
Wenn unity gestartet ist, klicken Sie auf den "Play-Button" oben in der Mitte. Nach einer kurzen Ladezeit startet die Anwendung.
Wenn das HMD richtig erkannt wurde, können Sie sich nun im virtuellen Klassenzimmer mittels Teleportation (Touchpad auf den Controllern) oder realer Positionsänderung fortbewegen.
Sollte kein HMD zur Verfügung stehen, können Sie die Anwendung mit WASD und der Maus durchlaufen.

### Starten des Frontend
1. Öffnen Sie auf in ihrem File-Explorer das Projekt VR-Klassenzimmer und navigieren Sie in den Ordner "website-control~".
2. Mit einem Rechtsklick in den Ordner rufen Sie nun die Git-bash auf. Es öffnet sich eine Konsole.
3. Stellen Sie sicher, dass NodeJS installiert ist mit "npm -v". Sollte dies keine Version von NodeJS anzeigen, installieren Sie NodeJS und starten Sie die Konsole neu.
4. Folgend installieren Sie in der Konsole mit der Zeile "npm install @reduxjs/toolkit" sowohl redux, als auch das toolkit mit allen notwendigen plugins.
Dies dauert einen Moment, danach können Sie die Konsole schließen.
5. Im website-control~ Ordner liegt eine Datei Namens "startCoach.sh", die Sie nun starten können. Nach einer kurzen Ladezeit öffnet sich Ihr Browser mit dem Frontend des Coachs.

Hier können Sie eine Position eines virtuellen Schülers / einer virtuellen Schülerin (vSus) anklicken.
Klicken Sie dann auf einen der Button unter dem Bild um eine Störung oder Mitarbeit zu starten.

Zum Beenden der Software klicken Sie in unity erneut den Play-Button.

## Entwicklerinfos
Das GitLab eigene Issue-Tracking-System bietet einen Überblick über laufende und geplante Entwicklungen. Es wird außerdem zur Erfassung von Störungen, Fehlern und Anfragen verwendet. 
Einen Startpunkt für die Entwicklung stellt das GitLab eigene Wiki dar.


Bei Fragen und Anmerkungen wenden Sie sich gern an wiepke@uni-potsdam.de.