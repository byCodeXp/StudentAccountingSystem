import { createClient, responseData, responseError } from './apiService';

const makeClient = () => {
   return createClient('category');
}

const fetchAll = async (request: { search: string }) => await makeClient().get('/', { params: request }).then(responseData).catch(responseError);

const fetchCreate = async (category: ICategory) => await makeClient().post('/create', category).then(responseData).catch(responseError);

const fetchUpdate = async (category: ICategory) => await makeClient().put('/update', category).then(responseData).catch(responseError);

const fetchDelete = async (id: string) => await makeClient().delete(`/delete/${id}`).then(responseData).catch(responseError);

const categoryApi = { fetchAll, fetchCreate, fetchUpdate, fetchDelete };

export default categoryApi;