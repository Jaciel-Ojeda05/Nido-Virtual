  export class PaginationEvent {
    constructor(
      public currentPage: number = 0,
      public itemsPerPage: number = 0
    ) {}
  }