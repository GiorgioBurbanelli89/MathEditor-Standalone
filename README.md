# Math Editor - Open Source MATLAB Alternative

**Una alternativa open source a MATLAB con sintaxis similar y salida matemática visual**

## 🚀 Características

- **Sintaxis MATLAB auténtica**: Variables sin `var`, funciones como `sin()`, `cos()`, `sqrt()`
- **Evaluación matemática real**: Cálculos precisos con soporte para variables y expresiones complejas
- **Salida visual matemática**: Símbolos reales (π, √, ², ³) con tipografía Times New Roman
- **Comportamiento semicolon**: Use `;` para suprimir salida, omítalo para mostrar resultados
- **Interfaz de doble panel**: Editor de código + Salida matemática visual

## 📸 Ejemplo de Uso

### Código MATLAB:
```matlab
% Variables (sin 'var')
x = 10;
y = 5;
result = x + y

% Funciones trigonométricas
sin(pi/2)
cos(0)
tan(pi/4)

% Potencias y raíces
2^8
sqrt(16)
3^2
```

### Salida Visual:
```
result = 15

sin(π/2) = 1
cos(0) = 1
tan(π/4) = 1

2⁸ = 256
√16 = 4
3² = 9
```

## 🛠️ Instalación y Uso

### Requisitos
- Windows 10/11
- .NET 8.0 SDK
- Visual Studio 2022 (recomendado)

### Instalación Rápida
1. Clona el repositorio:
   ```bash
   git clone https://github.com/GiorgioBurbanelli89/MathEditor-Standalone.git
   cd MathEditor-Standalone
   ```

2. Compila y ejecuta:
   ```bash
   dotnet run
   ```

3. ¡Empieza a escribir código MATLAB!

## 🎯 Funciones Soportadas

### Variables
```matlab
x = 5;          % Asignación oculta
y = 10          % Asignación visible
x               % Mostrar valor: x = 5
```

### Funciones Matemáticas
- **Trigonométricas**: `sin()`, `cos()`, `tan()`
- **Logarítmicas**: `log()`, `exp()`
- **Raíces**: `sqrt()`, `abs()`
- **Potencias**: `^` (ej: `2^8`, `x^2`)

### Constantes
- `pi` → π (3.14159...)
- `e` → e (2.71828...)

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Para contribuir:

1. Fork el proyecto
2. Crea una branch (`git checkout -b feature/NuevaCaracteristica`)
3. Commit tus cambios (`git commit -m 'Agregar nueva característica'`)
4. Push a la branch (`git push origin feature/NuevaCaracteristica`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver [LICENSE](LICENSE) para detalles.

## 📞 Contacto

**Giorgio Burbanelli**
- GitHub: [@GiorgioBurbanelli89](https://github.com/GiorgioBurbanelli89)

---

⭐ **¡Si te gusta este proyecto, dale una estrella en GitHub!** ⭐

*Math Editor - Haciendo las matemáticas accesibles para todos*
