export const SERVICE_UNAVAILABLE = 503;

export const statusCodeText = (code: number) => {
   switch (code) {
      case 200: {
         return 'Ok';
      }
      case 201: {
         return 'Created';
      }
      case 400: {
         return 'Bad request';
      }
      case 401: {
         return 'Unauthorized';
      }
      case 403: {
         return 'Forbidden';
      }
      case 404: {
         return 'Not found';
      }
      case 503: {
         return 'Server not respond';
      }
      default: {
         return 'NaN';
      }
   }
};
