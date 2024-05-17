const GetWeather = async (location) =>
{
    //Add extensive error handling here
    let data;
    let errorMessage = "";
    try {

        let response = await fetch(`api/weatherforecast/${location}`);
        let responseData = await response.json();
        errorMessage = responseData.errorMessage;
        data = responseData.data;
    } catch (e) {
        return {data: null, errorMessage: `Error retrieving data from the server. Error Message: ${e.Message}`}
    }

    return {data, errorMessage}
}


export { GetWeather }