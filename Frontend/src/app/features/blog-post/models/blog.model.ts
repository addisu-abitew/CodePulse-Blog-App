import { Category } from '../../category/models/category.model';

export interface Blog {
  id: string;
  title: string;
  urlHandle: string;
  shortDescription: string;
  content: string;
  featuredImageUrl: string;
  author: string;
  isVisible: Boolean;
  categories: Category[];
}
