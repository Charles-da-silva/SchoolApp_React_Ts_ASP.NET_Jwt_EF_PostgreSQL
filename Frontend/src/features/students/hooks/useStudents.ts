/*
Custom Hook

Responsável por:
- buscar dados
- controlar loading
- controlar erros
- expor estado para UI

A PAGE nunca fala direto com API.
Ela conversa apenas com HOOKS.
*/

import { useEffect, useState } from "react";
import { getStudents } from "../services/studentService";
import type { Student } from "../types/Student";

export function useStudents() {
    // dados
    const [students, setStudents] = useState<Student[]>([]);

    // estado de carregamento
    const [loading, setLoading] = useState(true);

    // ⭐ estado de erro
    const [error, setError] = useState<string | null>(null);

    async function loadStudents() {
        try {
            
            setLoading(true);
            setError(null);

            // Chamada para API através da camada de service
            const data = await getStudents();

            // Console de controle para verificar o que está chegando da API
            console.log("Tipo de data:", typeof data);
            console.log("Data:", data);
            console.log("É array?", Array.isArray(data));

            // Atualiza o estado com os dados recebidos, mas confere se existe data ou não.
            // Essa verificação evita crash
            setStudents(Array.isArray(data) ? data : []);
        
        } catch (err) {
            console.error(err);

            // mensagem amigável para UI
            setError("Erro ao carregar alunos.");

        } finally {
            // Independentemente de sucesso ou erro, removemos o loading
            setLoading(false);
        }
    }

    // useEffect executa ao montar página
    // O segundo parâmetro [] significa: execute apenas uma vez
    useEffect(() => {
            loadStudents();
    }, []);

    // Renderização principal
    return {
        students,
        loading,
        error,
        reload: loadStudents,
    };
}