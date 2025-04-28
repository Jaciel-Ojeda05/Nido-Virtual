export class AccesoResponse {
  constructor(
      public token: string = '',
      public nombre: string = '',
      public idUsuario: number = 0,
      public area: string = '',
      public correo: string = ''
  ) {}
}
