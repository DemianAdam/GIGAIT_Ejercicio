# Instrucciones de Implementación y Ejecución

**Proyecto: Gestión de Disponibilidad de Caja de Pago**


## Introducción

Este documento proporciona instrucciones detalladas para implementar y ejecutar el proyecto de Gestión de Disponibilidad de Caja de Pago. El proyecto consta de dos interfaces WPF implementadas siguiendo el patrón MVVM. Una interfaz se utiliza para gestionar la disponibilidad de la caja de pago 'x', y la otra guía a los usuarios en fila a las cajas disponibles.

## Requisitos del Sistema

Asegúrese de tener los siguientes componentes instalados en su entorno de desarrollo:

- Visual Studio (Versión recomendada: [Versión de Visual Studio])
- SQL Server (Versión máxima admitida: 2019)
- .NET Framework 4.6

## Pasos de Implementación

### 1. Configuración del Servicio WCF

- Abra el proyecto "Host" en Visual Studio, que sirve como el host para nuestro servicio WCF.
   
- Asegúrese de que el proyecto esté configurado correctamente para hostear el servicio. Verifique el archivo de configuración (app.config) y ajuste las rutas y enlaces si es necesario.

- Compile y ejecute el proyecto "Host" para poner en funcionamiento el servicio WCF.

### 2. Restauración de la Base de Datos

- Utilice el archivo de respaldo de la base de datos proporcionado para restaurar la base de datos SQL Server. Siga estos pasos utilizando SQL Server Management Studio:

   - Abra SQL Server Management Studio.
   - Conéctese al servidor SQL Server donde desea restaurar la base de datos.
   - Haga clic con el botón derecho en "Bases de datos" y seleccione "Restaurar base de datos."
   - Seleccione "Dispositivo" como origen y elija el archivo de respaldo (.bak) proporcionado.
   - Siga las instrucciones en pantalla para completar la restauración.

### 3. Configuración de la Conexión a la Base de Datos en WCF

- Asegúrese de que en el proyecto "WCF_Service," la cadena de conexión a la base de datos esté configurada correctamente en el archivo de configuración de WCF.

### 4. Ejecución de las Aplicaciones Cliente

- Asegúrese de que los proyectos de vista "Customer_View" y "Payment_Box_View" estén configurados correctamente y puedan consumir el servicio WCF. Actualice las referencias del servicio si es necesario.

- Proporcione instrucciones claras sobre cómo ejecutar cada proyecto de vista. Esto podría incluir la compilación de los proyectos y la ejecución de las aplicaciones cliente.

---
