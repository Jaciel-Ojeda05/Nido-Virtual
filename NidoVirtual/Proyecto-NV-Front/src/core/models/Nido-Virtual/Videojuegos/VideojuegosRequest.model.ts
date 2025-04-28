export class VideojuegosRequestModel {
    constructor(
        public titulo: string = "",
        public descVideojuego: string = "",
        public fechaLanzamiento: string = "",
        public precio: number = 0,
        public stock: number = 0,
        public idGenero: number = 0,
        public idPlataforma: number = 0,
        public imgVideojuego: string = ""
    ) { }
}