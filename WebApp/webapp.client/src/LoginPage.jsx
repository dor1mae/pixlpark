import { useEffect, useState } from 'react';
import {useNavigate } from 'react-router-dom'
import { SmartCaptcha } from '@yandex/smart-captcha';
import './App.css';

const SmartCaptchaComponent = (props) => {

    return <SmartCaptcha sitekey="ysc1_yHaxLPcpCD5tjMKBRqLm7A0VlL0AASPaDG3MRLqxacf72566" onSuccess={props.setToken} />;
}


function LoginPage() {

    const [isSend, setIsSend] = useState(false)
    const [email, setEmail] = useState("")
    const [code, setCode] = useState("")
    const [token, setToken] = useState('')
    const [isFailed, setIsFailed] = useState(false)
    const [confirmcode, setConfirmcode] = useState(null)

    const nav = useNavigate()

    const handleChangeEmail = (e) => {
        if(e != null)
        {
            setEmail(e.target.value)
        }
    }

    const handleChangeCode = (e) =>
    {
        if(e != null)
        {
            setCode(e.target.value)
        }
    }

    const handleButtonClick = () => {
        setIsSend(true)
        setConfirmcode(parseInt(Math.random() * (1100 - 101) + 101))
    }

    useEffect(() => {
        console.log(confirmcode)
        fetch(`http://localhost:5234/api/RabbitMq/SendMessage/Код:${confirmcode}, Почта:${email}`)
            .then(response => response.json())
            .then(data => console.log("ok"))
    }, [confirmcode])

    return (
        <div style={{flexDirection: 'column', display: "flex", gap: "10px"}}>
            {isSend ? (
                <div style={{flexDirection: 'column', display: "flex", gap: "10px"}}>
                    <label>
                        <span>Введите код, который был отправлен на почту </span>
                        <span>{email}</span>
                        </label>
                    <input
                    type="text"
                    value={code}
                        onChange={handleChangeCode} />
                    {isFailed && (
                        <span style={{color:'red'} }>Введен неправильный код</span>
                    ) }
                    <button onClick={() => {
                        if (code == confirmcode) {
                            setIsFailed(false)
                            nav(`/Success`)
                        }
                        else {
                            setIsFailed(true)
                        }
                    }}>
                        Подтвердить
                    </button>
                </div>
            ) : (
                <div style={{flexDirection: 'column', display: "flex", gap: "10px"}}>
                    <label>Введите почту для регистрации</label>
                    <input
                    type='email'
                    value={email}
                            onChange={handleChangeEmail} />

                    <SmartCaptchaComponent setToken={setToken}></SmartCaptchaComponent>
                    {token && (
                            <button onClick={() => {
                                handleButtonClick()
                            }}>Продолжить</button>
                    ) }
                </div>
            )}
        </div>
    );
}

export default LoginPage;