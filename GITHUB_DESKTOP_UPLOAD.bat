@echo off
echo ========================================
echo   SUBIR CON GITHUB DESKTOP
echo ========================================
echo.

cd "C:\Users\j-b-j\Projects\MathEditor-Standalone"

echo 1. Preparando repositorio Git...
git init
git add .
git commit -m "Initial commit: Math Editor - Open Source MATLAB Alternative"
git branch -M main

echo.
echo 2. Abriendo GitHub Desktop...

REM Intentar diferentes rutas de GitHub Desktop
if exist "C:\Users\j-b-j\AppData\Local\GitHubDesktop\GitHubDesktop.exe" (
    start "" "C:\Users\j-b-j\AppData\Local\GitHubDesktop\GitHubDesktop.exe" --open-repo "C:\Users\j-b-j\Projects\MathEditor-Standalone"
    goto :success
)

REM Buscar en subcarpetas app-*
for /d %%d in ("C:\Users\j-b-j\AppData\Local\GitHubDesktop\app-*") do (
    if exist "%%d\GitHubDesktop.exe" (
        start "" "%%d\GitHubDesktop.exe" --open-repo "C:\Users\j-b-j\Projects\MathEditor-Standalone"
        goto :success
    )
)

REM Si no encuentra GitHub Desktop
echo ❌ No se pudo abrir GitHub Desktop automáticamente
echo.
echo PASOS MANUALES:
echo 1. Abre GitHub Desktop manualmente
echo 2. File → Add Local Repository
echo 3. Selecciona: C:\Users\j-b-j\Projects\MathEditor-Standalone
echo 4. Click "Publish repository"
goto :end

:success
echo ✅ GitHub Desktop abierto
echo.
echo ========================================
echo PASOS EN GITHUB DESKTOP:
echo ========================================
echo.
echo 1. ✅ El proyecto ya está cargado
echo 2. Click "Publish repository" (botón azul)
echo 3. Configurar:
echo    📝 Name: MathEditor-Standalone
echo    📄 Description: Open Source MATLAB Alternative
echo    🌍 Public repository ✅
echo    🚫 Keep this code private ❌ (desmarcar)
echo 4. Click "Publish repository"
echo.
echo ========================================
echo 🎊 ¡LISTO! Tu repositorio estará en:
echo https://github.com/GiorgioBurbanelli89/MathEditor-Standalone
echo ========================================

:end
pause
