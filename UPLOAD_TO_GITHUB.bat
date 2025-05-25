@echo off
echo ========================================
echo   SUBIR MATH EDITOR A GITHUB
echo ========================================
echo.
echo Repositorio: MathEditor-Standalone
echo Usuario: GiorgioBurbanelli89
echo.

echo 1. Inicializando repositorio Git...
git init

echo 2. Configurando usuario Git...
git config user.name "Giorgio Burbanelli"
git config user.email "giorgio.burbanelli@ejemplo.com"

echo 3. Agregando archivos...
git add .

echo 4. Creando commit inicial...
git commit -m "Initial commit: Math Editor - Open Source MATLAB Alternative

✨ Características:
- Sintaxis MATLAB auténtica
- Evaluación matemática real
- Salida visual con símbolos matemáticos
- Interfaz WPF profesional

🛠️ Tecnologías:
- .NET 8.0 + WPF
- Parser matemático personalizado
- Renderizado visual de ecuaciones"

echo 5. Configurando branch principal...
git branch -M main

echo.
echo ========================================
echo PASOS MANUALES EN GITHUB:
echo ========================================
echo 1. Ve a: https://github.com/GiorgioBurbanelli89
echo 2. Click "New repository"
echo 3. Nombre: "MathEditor-Standalone"
echo 4. Descripción: "Open Source MATLAB Alternative"
echo 5. Público ✅
echo 6. Click "Create repository"
echo ========================================

set /p respuesta="¿Ya creaste el repositorio? (y/n): "

if /i "%respuesta%"=="y" (
    echo.
    echo 6. Conectando con GitHub...
    git remote add origin https://github.com/GiorgioBurbanelli89/MathEditor-Standalone.git
    
    echo 7. Subiendo archivos...
    git push -u origin main
    
    echo.
    echo ========================================
    echo ✅ ¡ÉXITO! Repositorio subido
    echo ========================================
    echo URL: https://github.com/GiorgioBurbanelli89/MathEditor-Standalone
    echo ========================================
) else (
    echo.
    echo Crea primero el repositorio en GitHub
    echo Luego ejecuta este script de nuevo
)

pause
