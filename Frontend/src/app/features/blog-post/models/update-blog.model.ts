import { Category } from '../../category/models/category.model';

export interface UpdateBlogRequest {
  title: string;
  urlHandle: string;
  shortDescription: string;
  content: string;
  featuredImageUrl: string;
  author: string;
  isVisible: Boolean;
  categories: Category[];
}
