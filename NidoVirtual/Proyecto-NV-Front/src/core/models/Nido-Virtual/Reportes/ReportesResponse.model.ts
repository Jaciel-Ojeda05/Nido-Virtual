export class ReportesResponseModel {
    constructor(
        public idVenta: number = 0,
        public idCliente: number = 0,
        public nombreUsuario: string = "",
        public idVideojuegos: number = 0,
        public videojuego: string = "",
        public cantidad: number = 0,
        public totalVentas: number = 0,
        public fechaVenta: string = ""
    ) { }
}