namespace ApiMesa.Domain.Enums
{
    public enum ERoles
    {
        Mozo,
        Cocinero,
        Manager
    }

    public enum EEstadosOrden
    {
        /// <summary>
        /// Recibida por el mozo, esperando a cocinero.
        /// </summary>
        Pendiente,
        /// <summary>
        /// Tomada por el cocinero, esperando finalizacion.
        /// </summary>
        EnPreparacion,
        /// <summary>
        /// Cancelada por el mozo.
        /// </summary>
        Cancelada,
        /// <summary>
        /// Finalizada por el cocinero, servida por el mozo.
        /// </summary>
        Lista,
        /// <summary>
        /// Abonada por el comenzal.
        /// </summary>
        Cerrada
    }

    public enum EEstadosMesa
    {
        /// <summary>
        /// Nueva orden creada.
        /// </summary>
        Ocupada,
        /// <summary>
        /// Orden cerrada
        /// </summary>
        Disponible,
        Reservada
    }

    public enum ECategoria
    {
        Drink,
        Dish
        ///Garnish
    }
}
