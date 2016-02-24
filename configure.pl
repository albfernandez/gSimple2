#!/usr/bin/perl -w -s

# Opciones pasadas por línea de comandos.
# Valores válidos:
# 	-help: muestra la ayuda
#	-debug: para compilar la aplicación con información de depuración.
#	-test: para compilar la aplicación con información de depuración y con los tests.
#	-release: para compilar la apliación sin información de depuración (parámetro por defecto).

if (not $help){
	$help = 0;
}
if (not $debug){
	$debug = 0;
}
if (not $test){
	$test = 0;
}
if (not $release){
	$release = 0;
}

if ($test)
{
	$fuentes = "-recurse:src/simple2/*.cs -recurse:src/tests/*.cs";
	$mcsflags = "-g -r NUnit.Framework -warn:4 -nowarn:0219,0168";
	$monoflags = "--debug";
	$fuentesdep = "\$(shell find src/simple2 -name *.cs) \$(shell find src/tests/ -name *.cs)";
}
elsif ($debug)
{
	$fuentes = "-recurse:src/simple2/*.cs";
	$mcsflags = "-g -warn:4 -nowarn:0219,0168";
	$monoflags = "--debug";
	$fuentesdep = "\$(shell find src/simple2 -name *.cs)"
}
else
{
	$fuentes = "-recurse:src/simple2/*.cs";
	$mcsflags = "-warn:4 -nowarn:0219,0168";
	$monoflags = "-O=all";
	$fuentesdep = "\$(shell find src/simple2 -name *.cs)";
}

if ($help){
	mostrar_ayuda();
	exit 0;
}


# Buscamos el compilador (Sólo mcs)

printf("Buscando el compilador mcs...");

$mcs = esta_en_el_path("mcs");

if (not $mcs){
	printf ("\nNo fue posible encontrar mcs.\n");
	printf ("Visite http://www.go-mono.org para descargar e instalar Mono\n\n");
	exit -1
}
else{
	printf ("Encontrado\n");
	$compiler = "mcs";
}

# Buscamos las librerias de Gtk#. 

printf("Buscando las librerías Gtk#...");

$gtkdll_path = "";
$libdirs = find_libdirs();
@libdirs = split ":", $libdirs;
foreach $libdir (@libdirs) {
    if (!$gtkdll_path and $res = is_file_in_dir("gdk-sharp.dll", $libdir)) {
        printf(" Encontradas en " . $libdir ."\n");
        $gtkdll_path = $libdir;
    }
}

if (not $gtkdll_path) {
	printf ("No encontradas\n");
    printf("\nNo se han podido encontrar Gtk# (en " . $libdirs . ")\n");
	printf("Necesita tener instaladas estas librerías para poder compilar \n");
	printf ("la aplicación. Instalelas y hágalas accesibles poniendolas en \n");
	printf ("MONO_PATH\n");
    exit -1;
}


# Se necesita un runtime de .NET. De momento solo mono.

printf("Buscando \"runtime\" mono...");
if (!esta_en_el_path("mono")) {
	printf ("No encontrado.\n");
    printf("\nNecesita mono para ejecutar la aplicación. Visite http://www.go-mono.org\n");
    exit(-1);
} else {
	$runtime="mono";
	printf(" Encontrado.\n");
}

#Buscamos monoresgen.exe

printf ("Buscando monoresgen.exe...");

@path = split ":", $ENV{PATH} ;
foreach $p (@path) {
	if ($res = is_file_in_dir("monoresgen.exe", $p)) {
		$resgen = $res;
	}
}
if (!$resgen){
	printf ("No encontrado\n");
	printf ("\nmonoresgen.exe no encontrado\n");
	exit (-1);
}
else 
{
	printf (" Encontrado en " . $resgen . "\n");
}

# Construimos la cadena a pasar al compilador para utilizar los
# recursos de gtk.

$gtk_resources = build_gtk_resources_string($gtkdll_path);


# Buscamos los recursos de traducciones.

printf ("Buscando las traducciones instaladas...");
$tr = idiomas_disponibles ("src/languages");
print (" " . $tr . "\n");
$traducciones = generarTraducciones ($tr);
$recursosTraducciones = generarRecursosTraducciones ($tr);


# Buscamos csdoc y csdoc2html
printf ("Buscando csdoc...");

$csdoc = esta_en_el_path("csdoc");

if (not $csdoc){
	printf ("No encontrado.\n");
}
else{
	printf ("Encontrado\n");
}
printf ("Buscando csdoc2html...");
$csdoc2html = esta_en_el_path ("csdoc2html");

if (not $csdoc2html){
	printf("No encontrado.\n");
}
else{
	printf ("Encontrado\n");
}
if ( (not $csdoc) or (not $csdoc2html))
{
	$comcsdoc="echo No tiene instalado csdoc (de pnet)."
}
else
{
	$comcsdoc = "csdoc -w -fprivate -gtk -o docs/doc.xml \$(FUENTES_COMENTAR)". 
	" && csdoc2html -ftitle=\"gSimple2\" -o docs/ docs/doc.xml"
}


# Construimos el Makefile

open MAKEFILE_TEMPLATE, "Makefile.in" or die("\nNo pude encontrar Makefile.in\n");

open MAKEFILE, ">Makefile" or die("\nNo pude abrir Makefile para escribir\n\n\n");

@makefile = <MAKEFILE_TEMPLATE>;
$makefile = join "", @makefile;

$makefile =~ s/\@RUNTIME\@/$runtime/;
$makefile =~ s/\@COMPILER\@/$compiler/;
$makefile =~ s/\@GTK_RESOURCES\@/$gtk_resources/;
$makefile =~ s/\@MONO_RESGEN\@/$resgen/;
$makefile =~ s/\@TRADUCCIONES\@/$traducciones/;
$makefile =~ s/\@RECURSOS_TRADUCCIONES\@/$recursosTraducciones/;
$makefile =~ s/\@MONOFLAGS\@/$monoflags/;
$makefile =~ s/\@MCSFLAGS\@/$mcsflags/;
$makefile =~ s/\@FUENTES\@/$fuentes/;
$makefile =~ s/\@FUENTES_DEPENDENCIAS\@/$fuentesdep/;
$makefile =~ s/\@CSDOC\@/$comcsdoc/;

print MAKEFILE "$makefile";


printf("\n\nMakefile generado.\n");
printf ("Teclee 'make' para construir gSimple2\n");
printf ("o 'make run' para construirlo y probarlo.\n");

###############################################################################
#
# Funciones
#
###############################################################################


#Obtiene una lista con los ficheros de recursos de traducciones.
sub generarTraducciones
{
	my ($cadena) = @_;
	my ($trad) = "";
	my (@array) = split " ", $cadena;
	foreach (@array){
		$trad .= "build/Mensajes." . $_ . ".resources ";
	
	}	
	return $trad;
}
#Obtiene los parámetros a pasarle para incluir los recursos de traducciones
#en el "assembly" principal.
sub generarRecursosTraducciones 
{
	my ($cadena) = @_;
	my ($trad) = "";
	my (@array) = split " ", $cadena;
	foreach (@array){
		$trad .= "-resource:build/Mensajes." . $_ . ".resources,Mensajes.".$_.".resources ";
	
	}	
	return $trad;
}

#Obtiene una lista con los idiomas disponibles.
#ejemplo de lista [es, es_ES, en]
sub idiomas_disponibles
{
	my ($dir) = @_;
	opendir(DIR, $dir);

	@files = sort(grep(/^Mensajes\.([A-Za-z_\-]+)\.txt$/, readdir(DIR)));

	closedir(DIR);
	my $cadena;
	my ($lenguajes) = "";
	my ($i);
	$i = 0;


	foreach (@files){
		$cadena = $_;
		$cadena =~ s/Mensajes.//;
		$cadena =~ s/.txt//;
		$lenguajes .= $cadena . " ";
		$i++;
	}
	return $lenguajes;
}



# Devuelve el path absoluto del programa si el programa está en el PATH, 
# 0 en otro caso

sub esta_en_el_path
{
    my ($prog_name) = @_;
    
    return is_file_in_path_like_string($prog_name, $ENV{PATH});
}

# Devuelve el path absoluto del fichero si $file está en $dir, 0 en otro caso

sub is_file_in_dir
{
    my ($file, $dir) = @_;

    my $full_path = $dir . "/" . $file;
    if (-f $full_path) {
        return $full_path;
    } else {
        return 0;
    }
}

# Devuelve el path absoluto del fichero si está en uno de los directorios
# (separados por :) indicados.

sub is_file_in_path_like_string
{
    my ($file, $path_like_string) = @_;
    my @path = split ":", $path_like_string;
   
    foreach my $dir (@path) {
        if ($ret = is_file_in_dir($file, $dir)) {
            return $ret;
        }
    }
    return 0;
}

# Nos devuelve los directorios en los que debemos buscar las librerías.
sub find_libdirs
{
    my $ret1 = "/usr/lib";
    my $ret2 = "";
    $ret1 .= $ENV{MONO_PATH} unless not $ENV{MONO_PATH};
    $ret2 .= $ENV{LD_LIBRARY_PATH} unless not $ENV{LD_LIBRARY_PATH};
    if ($ret1) {
        $ret2 = $ret1 . ":" . $ret2;
    }
    return $ret2;
}


# Construye una 'string' con las opciones que hay que pasar al compilador
# para linkar las librerías de Gtk#.

sub build_gtk_resources_string
{
    my ($gtk_dir) = @_;

    if ($gtk_dir eq "") {
        return "";
    }
	return "-r gtk-sharp -r glib-sharp -r gdk-sharp -r pango-sharp";
}

# Muestra las opciones disponibles para el configure

sub mostrar_ayuda
{
	printf ("configure\n");
	printf ("Opciones disponibles:\n\n");
	printf ("-help:   Muestra esta ayuda.\n");
	printf ("-debug:  Compila la aplicación con información de depuración.\n");
	printf ("-test:   Compila la aplicación con información de depuración.\n");
	printf ("         y con los test.\n");
	printf ("-release:Compila la aplicación sin información de depuración. (por defecto).");
	printf ("\n\n\n\n\n");
	
}
