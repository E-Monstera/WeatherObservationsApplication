import { useEffect, useState } from 'react';
import './App.css';

function App() {

    const [location, setLocation] = useState();
    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState();
    const [error, setError] = useState(false);
    const [weatherData, setWeatherData] = useState();

    //Maintain updated state for user input
    const handleChange = (e) => {
        setLocation(e.target.value);
    }

    //Handle submission of the form
    const handleSubmit = async () => {
        setIsLoading(true);

        //Reset values from previous call
        setIsError(false);
        setError(false);
        setData();

        //If this were C#, I would also add ways in which the user can cancel the request and
        //handling for when a user attemps to press the button multiple times.
        //As I'm just getting back to working with Javascript, I still need a refresher on the syntax
        //for handling this in Javascript
        try {
            var res = await fetch("api/weatherforecast");
            setWeatherData(res.data);
            setIsError(!res.success);
            setError(res.errorMessage);
        } catch (e) {
            setIsError(true);
            setError(e.errorMessage);
        } finally {
            setIsLoading(false);
        }


    }



    return (
        <>
            <h1>Weather Observations</h1>
            <div>
                <form onSubmit={async () => await handleSubmit()}>
                    <label htmlFor='location'>Please enter a location:</label>
                    <input type='text' id='location' autoFocus name='location' onChange={handleChange} />
                    <button type='submit'>Search Location</button>
                </form>
            </div>
            <div>
                {isLoading && <h1>Loading....</h1>}
                {isError && <h1>Error encountered. Please try again.</h1>}
                {/*<table>*/}
                {/*    <thead>*/}
                {/*        <tr>*/}
                {/*            <th>Date</th>*/}
                {/*            <th>High Temperature</th>*/}
                {/*            <th>Low Temperature</th>*/}
                {/*        </tr>*/}
                {/*    </thead>*/}
                {/*    <tbody>*/}
                {/*        {weatherData.map(data =>*/}
                {/*            <tr key={data.Date}>*/}
                {/*                <td>{data.Date}</td>*/}
                {/*                <td>{data.HighTemperature}</td>*/}
                {/*                <td>{data.LowTemperature}</td>*/}
                {/*            </tr>*/}
                {/*        )}*/}
                {/*    </tbody>*/}
                {/*</table>*/}


            </div>



        </>
    )
}

export default App;