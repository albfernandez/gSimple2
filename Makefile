# Makefile

TARGET=gSimple2.exe
MCS=mcs
MCS_FLAGS=-warn:4 -nowarn:0219,0168
GTK_RESOURCES=-r gtk-sharp -r glib-sharp -r gdk-sharp -r pango-sharp

MONO=mono
MONO_FLAGS=-O=all
RESGEN=$(MONO) /usr/bin/monoresgen.exe
BUILDDIR=build

TRADUCCIONES=build/Mensajes.en.resources build/Mensajes.es.resources 
RECURSOS_TRADUCCIONES=-resource:build/Mensajes.en.resources,Mensajes.en.resources -resource:build/Mensajes.es.resources,Mensajes.es.resources 
PIXMAPS=$(BUILDDIR)/Pixmaps.resources
RESOURCES=$(RECURSOS_TRADUCCIONES) -resource:$(PIXMAPS),Pixmaps.resources

FUENTES_COMENTAR=$(shell find src/simple2 -name "*.cs")
FUENTES_DEPENDENCIAS=$(shell find src/simple2 -name *.cs)
FUENTES=-recurse:src/simple2/*.cs
AUXRESGEN=$(BUILDDIR)/Files2Resource.exe

LINEA = "======================================================================"

all: $(TARGET)

run: $(TARGET)
	$(MONO) $(MONO_FLAGS) $(TARGET)


$(TARGET): $(PIXMAPS) $(TRADUCCIONES) $(FUENTES_DEPENDENCIAS)
	@echo $(LINEA) 
	@echo "Compilando la aplicación $(TARGET)."
	@echo $(LINEA)
	$(MCS) $(GTK_RESOURCES) $(RESOURCES) $(MCS_FLAGS) -o $(TARGET) $(FUENTES) 


#Construcción de los recursos de imágenes.

$(PIXMAPS): $(AUXRESGEN) pixmaps/*
	@echo $(LINEA)
	@echo "Generando los recursos de imágenes."
	@echo $(LINEA)
	$(MONO) $(MONO_FLAGS) $(AUXRESGEN) pixmaps/* $(PIXMAPS)

$(AUXRESGEN): src/soporte/Files2Resource.cs
	@echo $(LINEA)
	@echo "Compilando generador de recursos."
	@echo $(LINEA)
	install -d $(BUILDDIR)
	$(MCS) $(MCS_FLAGS) -o $(AUXRESGEN) src/soporte/Files2Resource.cs

#Construcción de los paquetes del lenguaje.

$(TRADUCCIONES):build/Mensajes.%.resources: src/languages/Mensajes.%.txt
	@echo $(LINEA)
	@echo "Generando los recursos de lenguajes: $@."
	@echo $(LINEA)
	install -d build
	$(RESGEN) $< $@


#Documentación
docs:
	install -d docs
	csdoc -w -fprivate -gtk -o docs/doc.xml $(FUENTES_COMENTAR) && csdoc2html -ftitle="gSimple2" -o docs/ docs/doc.xml

#Elimina los archivos generados al compilar
clean:
	rm -f $(TARGET)
	rm -rf build/
	rm -rf docs/

	
.PHONY: clean docs
