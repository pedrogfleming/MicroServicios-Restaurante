# MicroServicios-Restaurante

In this project, I implemented:

Microservice ASP .NET APIs
Entity Framework
MySql Database(Using Pomelo)
Ocelot API Gateway
Custom Generic Mapper using Reflection Classes
XUnit Testing(Using Moq and FluentAssertions)
Loggin(Serilog)

Design Patterns:

Repository Pattern(Using a generic repository)
Unit Of Work Pattern
Mediator(using MediatR)
CQRS
Http Manager to simplify the communications between the APIs


Objetivo
Se busca como meta final construir un grupo de microservicios que permitan brindar las
funcionalidades esperadas para el sistema de backend sugerido, con la mejor calidad posible.

Arquitectura Descripción a alto nivel
El sistema debe simular el backend de un sector de un restaurant por lo que se pide algunas
funcionalidades mínimas para poder funcionar y brindar las capacidades necesarias al frontend.
Requerimiento de negocio
Autenticacion de Empleados
● Se desea identificar quién utiliza la plataforma, para esto se debe autenticar inicialmente
cada empleado con nombre usuario y contraseña para obtener el rol y permisos
correspondientes.
Roles: Mozo - Cocinero - StockManager
Gestión de Mesas
3
IMPORTANTE: Partiendo de lo desarrollado en el práctico anterior, se debe tener en cuenta de
que cada Orden debe estar asignada a una Mesa con los siguientes datos como mínimamente
(Identificador, mozo responsable(nombre usuario), estado, cantidad de personas y Orden)
● Cada Mozo debe poder
○ Abrir una mesa. (Ver tema identificador unico)
○ Consultar disponibilidad de Menú.
○ Generar orden a una mesa.
○ Agregar items a la Orden.
○ Consultar el estado de una orden.
○ Quitar ítem de una orden específica siempre y cuando no esté en preparación.
○ Solicitar el cierre de mesa teniendo en cuenta que la propina es del 10%
obligatoria.
○ Abonar Mesa (reutilizar formas de pago).
○ Consultar propina obtenida. (cada mesa deja un 10% de propina)
○ Al iniciar la preparación de la orden se deben actualizar el stock de los menú
Items del inventario.
Gestion de Ordenes
● Los Cocineros deben poder
○ Devolver el estado de una orden.
○ Actualizar estado de una Orden.(Pendiente, En preparación y Lista)
○ Devolver listado de ordenes (mesa, mozo, fecha, etc)
○
Gestion de Inventario
● Los managers deben poder
○ Devolver la disponibilidad de la lista de menú Items.
○ Actualizar el stock de uno o varios menuItem específico
○ Agregar/ Eliminar/Editar menu Items.
○ Actualizar costos de forma masiva por porcentaje y/o por tipo.
4
Se Asume que:
● El inventario se expresa en productos finalizados (por ej: papas con verdeo, ensalada 3
ingredientes, etc)
● Cada mesa tiene una única orden en simultáneo.
● Cada mesa tiene un identificador único, por cada vez que se atiende a alguien.