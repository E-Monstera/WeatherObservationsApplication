import { useEffect, useState } from 'react';
import './App.css';

function App() {

    const [location, setLocation] = useState();
    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState();
    const [error, setError] = useState(false);
    const [data, setData] = useState();


    const handleChange = (e) => {
        setLocation(e.target.value);
    }

    const handleSubmit = async () => {
        setIsLoading(true);

        //Reset values from previous call
        setIsError(false);
        setError(false);
        setData();

        //If this were C# I would also create a CancellationToken here and pass it in to this
        //so that we can easily cancel the request. I am not confident in the Javascript syntax for this though.
        try {
            var res = await fetch("api/weatherforecast");
            setData(res.data);
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
            <h1>Nice Title</h1>
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



            </div>



        </>
    )
}

export default App;