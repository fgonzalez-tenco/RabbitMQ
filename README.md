# RabbitMQ Demo Project

Este proyecto demuestra la implementación de un sistema de mensajería usando RabbitMQ con .NET 8, compuesto por una aplicación productora y una aplicación consumidora.


## ¿Qué hacen las aplicaciones?

### ProducerApp (Productor)
- **Propósito**: Envía mensajes a RabbitMQ
- **Funcionalidad**: 
  - Se conecta a RabbitMQ usando la librería `Simone.Common.RabbitMQ`
  - Publica un mensaje "Hello, RabbitMQ!" con un CorrelationId único
  - Espera confirmación del usuario antes de terminar

### ConsumerApp (Consumidor)
- **Propósito**: Escucha y procesa mensajes de RabbitMQ
- **Funcionalidad**:
  - Se mantiene ejecutándose continuamente
  - Escucha mensajes de la cola configurada
  - Registra cada mensaje recibido con su CorrelationId
  - Envía acknowledgment automático de mensajes procesados

## Configuración

### Requisitos Previos
- .NET 8.0 SDK
- Servidor RabbitMQ ejecutándose
- Acceso a las credenciales de RabbitMQ

### Configuración de RabbitMQ

Ambas aplicaciones requieren configurar el archivo `appsettings.json` con las credenciales y configuración de RabbitMQ:

#### ProducerApp/appsettings.json
```json
{
  "RabbitMQ": {
    "Host": "localhost",          // IP o dominio del servidor RabbitMQ
    "Username": "rabbitMQ_user",   // Usuario de RabbitMQ
    "Password": "rabbitMQ_password",  // Contraseña
    "Exchanges": [
      "RabbitMQ-Test"            // Exchange a utilizar
    ],
    "Errors": {
      "Name": "Errors",
      "Exchange": "RabbitMQ-Errors"
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

#### ConsumerApp/appsettings.json
```json
{
  "RabbitMQ": {
    "Host": "localhost",
    "Username": "rabbitMQ_user",
    "Password": "rabbitMQ_password",
    "Queues": {
      "RabbitMQ-Test": "RabbitMQ-Test-Consumer"  // Mapeo cola-consumidor
    },
    "Errors": {
      "Name": "Errors",
      "Exchange": "RabbitMQ-Errors"
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

### Parámetros de Configuración

| Parámetro | Descripción | Ejemplo |
|-----------|-------------|---------|
| `Host` | Dirección IP o dominio del servidor RabbitMQ | `localhost` o `172.100.0.88` |
| `Username` | Usuario para autenticación en RabbitMQ | `simone_admin` |
| `Password` | Contraseña del usuario | `c6YmN5CljhiVL416OfE6` |
| `Exchanges` | Lista de exchanges para el productor | `["RabbitMQ-Test"]` |
| `Queues` | Mapeo de colas para el consumidor | `{"RabbitMQ-Test": "RabbitMQ-Test-Consumer"}` |


> [!TIP]
> En caso de que se esté ejecutando RabbitMQ desde Simone.Infrastructure, se puede utilizar localhost o la IP expuesta por el contenedor.


## Cómo Ejecutar

### 1. Compilar los proyectos
```bash
dotnet build ConsumerApp/ConsumerApp.csproj
dotnet build ProducerApp/ProducerApp.csproj
```

### 2. Ejecutar el Consumidor (Terminal 1)
```bash
cd ConsumerApp
dotnet run
```
El consumidor se mantendrá ejecutándose y mostrará logs cuando reciba mensajes.

### 3. Ejecutar el Productor (Terminal 2)
```bash
cd ProducerApp
dotnet run
```
El productor enviará un mensaje y esperará que presiones una tecla para terminar.

## Ejemplo de Ejecución

**Terminal 1 (Consumidor):**
```
info: Microsoft.Hosting.Lifetime[0]
      Application started.
info: ConsumerApp.Consumer[0]
      [abc123xyz] Mensaje recibido: Hello, RabbitMQ!
```

**Terminal 2 (Productor):**
```
info: ProducerApp.Publisher[0]
      [abc123xyz] Message Hello, RabbitMQ! sent successfully.
Mensaje enviado. Presiona cualquier tecla para salir...
```

## Dependencias

### Paquetes NuGet Utilizados
- `Simone.Common.RabbitMQ` (v2.8.1) - Cliente RabbitMQ
- `Microsoft.Extensions.Hosting` (v8.0.0) - Hosting para aplicaciones .NET

## Solución de Problemas

### Error: RabbitMQNotConnected
- Verificar que el servidor RabbitMQ esté ejecutándose
- Confirmar que Host, Username y Password son correctos
- Verificar conectividad de red al servidor RabbitMQ

### Error: Configuration file not found
- Ejecutar desde el directorio correcto (`cd ConsumerApp` o `cd ProducerApp`)

### No se ven logs del Consumer
- Verificar nivel de logging en `appsettings.json`
