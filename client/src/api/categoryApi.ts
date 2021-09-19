import { createClient, responseData, responseError } from './apiService';

const client = createClient('category');

const fetchAll = async () => await client.get('/').then(responseData).catch(responseError);

const fetchCreate = async (category: ICategory) => await client.post('/create', category).then(responseData).catch(responseError);

const fetchUpdate = async (category: ICategory) => await client.put('/update', category).then(responseData).catch(responseError);

const fetchDelete = async (id: string) => await client.delete(`/delete/${id}`).then(responseData).catch(responseError);

const categoryApi = { fetchAll, fetchCreate, fetchUpdate, fetchDelete };

export default categoryApi;