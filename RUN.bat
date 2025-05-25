@echo off
echo ========================================
echo   Ejecutando Math Editor
echo ========================================
echo.

if exist "bin\Debug\net8.0-windows\MathEditor.exe" (
    echo ✅ Ejecutable encontrado
    echo Iniciando Math Editor...
    echo.
    start "" "bin\Debug\net8.0-windows\MathEditor.exe"
    echo ✅ Math Editor iniciado
) else (
    echo ❌ Ejecutable no encontrado
    echo.
    echo Compilando proyecto...
    dotnet build
    
    if exist "bin\Debug\net8.0-windows\MathEditor.exe" (
        echo ✅ Compilación exitosa
        echo Iniciando Math Editor...
        start "" "bin\Debug\net8.0-windows\MathEditor.exe"
    ) else (
        echo ❌ Error en la compilación
        echo Abre el proyecto en Visual Studio y compila manualmente
    )
)

echo.
pause
