export class VideojuegosResponseModel {
    constructor(
        public idVideojuegos: number = 0,
        public titulo: string = "",
        public descVideojuego: string = "",
        public fechaLanzamiento: string = "",
        public precio: number = 0,
        public stock: number = 0,
        public descGenero: string = "",
        public descPlataforma: string = "",
        public imgVideojuego: string = "" 
    ) { }
}