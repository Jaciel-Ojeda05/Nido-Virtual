export class MenuModel {
    constructor(
      public icon: string = '',
      public label: string = '',
      public route: string = '',
      public submenu: MenuModel[] = new Array<MenuModel>()
    ) {}
  }

  