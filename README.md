# StreamProblem
Demo für das Stream Problem

# Url für den Change
https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/partial-byte-reads-in-streams

# Anleitung
Um die Änderung zu sehen muss Framework Version in Projekt zwischen 5 und 6 geändert werden
5 -> ProcessZipNet5 und ProcessZipNet6 Produzieren den selben Output
6 -> ProcessZipNet5 beinhaltet Nullen, ProcessZipNet6 hat diese nicht

