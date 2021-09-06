import { createClient, responseData, responseError } from './apiService';

const client = createClient('category');

const fetchCategories = async () => await client.get('/').then(responseData).catch(responseError);

const fetchOneCategory = async (id: string) =>
   await client.get(`/${id}`).then(responseData).catch(responseError);

const fetchCreateCategory = async (category: ICategory) =>
   await client.post('/create', category).then(responseData).catch(responseError);

const fetchUpdateCategory = async (id: string, category: ICategory) =>
   await client.put(`/update/${id}`, category);

const fetchDeleteCategory = async (id: string) =>
   await client.delete(`/delete/${id}`).then(responseData).catch(responseError);

const categoryApi = {
   fetchCategories,
   fetchOneCategory,
   fetchCreateCategory,
   fetchUpdateCategory,
   fetchDeleteCategory,
};

export default categoryApi;
