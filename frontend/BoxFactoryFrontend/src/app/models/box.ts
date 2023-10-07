// box.model.ts
export interface Box {
  id?: number;
  boxName: string;
  price: number;
  boxWidth: number;
  boxLength: number;
  boxHeight: number;
  boxThickness: number;
  boxColor?: string;
  boxImgUrl?: string;
}

// search-box.model.ts
export interface SearchBox {
  boxName?: string;
  price?: number;
  boxColor?: string;
  boxImgUrl?: string;
}
